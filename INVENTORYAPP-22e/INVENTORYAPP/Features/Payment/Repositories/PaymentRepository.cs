using INVENTORYAPP.Data;
using INVENTORYAPP.Features.Masters.Accounts.DTOs;
using INVENTORYAPP.Features.Payments.DTOs;
using INVENTORYAPP.Features.Payments.Interface;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Payments.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
    {
        _context = context;
    }

    //====================================================
    // Party Lookup
    // AccountMaster: C = Customer, S = Supplier
    //====================================================

    public async Task<List<PaymentPartyLookupResponse>> GetPartiesAsync()
    {
        return await _context.Accounts
            .Where(x => x.IsActive && !x.IsDeleted &&
                        (x.Actype == "C" || x.Actype == "S"))
            .OrderBy(x => x.AccountName)
            .Select(x => new PaymentPartyLookupResponse
            {
                Id = x.Id,
                PartyCode = x.AccountCode,
                PartyName = x.AccountName
            })
            .ToListAsync();
    }

    //====================================================
    // Account Lookup
    // AccountMaster: B = Bank/Cash
    //====================================================

    public async Task<List<AccountLookupResponse>> GetAccountsAsync()
    {
        return await _context.Accounts
            .Where(x => x.IsActive && !x.IsDeleted && x.Actype == "B")
            .OrderBy(x => x.AccountName)
            .Select(x => new AccountLookupResponse
            {
                Id = x.Id,
                AccountCode = x.AccountCode,
                AccountName = x.AccountName,
                Actype = x.Actype
            })
            .ToListAsync();
    }

    //====================================================
    // Save / Update Payment
    //====================================================

    public async Task<long> SavePaymentAsync(SavePaymentDto dto)
    {
        if (dto.Details == null || dto.Details.Count == 0)
            throw new Exception("At least one payment detail is required.");

        if (dto.Details.Any(x => x.Amount <= 0))
            throw new Exception("Payment amount must be greater than zero.");

        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            // Party must come from AccountMaster and must be C or S.
            var party = await _context.Accounts
                .FirstOrDefaultAsync(x =>
                    x.Id == dto.PartyId &&
                    x.IsActive &&
                    !x.IsDeleted &&
                    (x.Actype == "C" || x.Actype == "S"));

            if (party == null)
            {
                throw new Exception(
                    $"Party not found in AccountMaster. PartyId = {dto.PartyId}");
            }

            PaymentEntry? payment = await _context.PaymentEntries
                .FirstOrDefaultAsync(x => x.DocNo == dto.DocNo);

            if (payment != null)
            {
                // UPDATE
                payment.DocDate = dto.DocDate;
                payment.PartyId = dto.PartyId;
                payment.STimestamp = DateTime.Now;
                payment.ModifiedOn = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var oldDetails = await _context.PaymentEntryDetails
                    .Where(x => x.DocNo == payment.DocNo)
                    .ToListAsync();

                _context.PaymentEntryDetails.RemoveRange(oldDetails);
                await _context.SaveChangesAsync();
            }
            else
            {
                // INSERT
                payment = new PaymentEntry
                {
                    DocDate = dto.DocDate,
                    PartyId = dto.PartyId,
                    STimestamp = DateTime.Now
                };

                _context.PaymentEntries.Add(payment);
                await _context.SaveChangesAsync();
            }

            int slNo = 1;

            foreach (var item in dto.Details)
            {
                // Detail Account must be Bank/Cash (B).
                var account = await _context.Accounts
                    .FirstOrDefaultAsync(x =>
                        x.Id == item.AccountId &&
                        x.IsActive &&
                        !x.IsDeleted &&
                        x.Actype == "B");

                if (account == null)
                {
                    throw new Exception(
                        $"Bank/Cash account not found in AccountMaster. AccountId = {item.AccountId}");
                }

                var detail = new PaymentEntryDetail
                {
                    DocNo = payment.DocNo,
                    DocDate = dto.DocDate,
                    PartyId = dto.PartyId,
                    STimestamp = DateTime.Now,
                    SlNo = slNo++,
                    AccountId = item.AccountId,
                    AcName = account.AccountName,
                    Amount = item.Amount
                };

                _context.PaymentEntryDetails.Add(detail);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return payment.DocNo;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    //====================================================
    // Next Payment Number
    //====================================================

    public async Task<long> GetNextPaymentNumberAsync()
    {
        var lastDocNo = await _context.PaymentEntries
            .OrderByDescending(x => x.DocNo)
            .Select(x => (long?)x.DocNo)
            .FirstOrDefaultAsync();

        return (lastDocNo ?? 0) + 1;
    }

    //====================================================
    // Search
    // AccountMaster is the Party source.
    //====================================================

    public async Task<List<PaymentSearchDto>> SearchAsync(string keyword)
    {
        keyword = keyword?.Trim().ToLower() ?? string.Empty;

        return await (
            from payment in _context.PaymentEntries
            join account in _context.Accounts
                on payment.PartyId equals account.Id
            where
                payment.DocNo.ToString().Contains(keyword)
                || account.AccountCode.ToLower().Contains(keyword)
                || account.AccountName.ToLower().Contains(keyword)
                || payment.DocDate.ToString().Contains(keyword)
            select new PaymentSearchDto
            {
                DocNo = payment.DocNo,
                DocDate = payment.DocDate,
                PartyCode = account.AccountCode,
                PartyName = account.AccountName
            })
            .Distinct()
            .OrderByDescending(x => x.DocNo)
            .ToListAsync();
    }

    //====================================================
    // Get Payment
    //====================================================

    public async Task<PaymentDto?> GetPaymentByDocNoAsync(long docNo)
    {
        var payment = await _context.PaymentEntries
            .FirstOrDefaultAsync(x => x.DocNo == docNo);

        if (payment == null)
            return null;

        var details = await _context.PaymentEntryDetails
            .Where(x => x.DocNo == docNo)
            .OrderBy(x => x.SlNo)
            .Select(x => new PaymentDetailDto
            {
                AccountId = x.AccountId,
                Amount = x.Amount
            })
            .ToListAsync();

        return new PaymentDto
        {
            DocNo = payment.DocNo,
            DocDate = payment.DocDate,
            PartyId = payment.PartyId,
            Details = details
        };
    }

    //====================================================
    // Delete Payment
    //====================================================

    public async Task DeletePaymentAsync(long docNo)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            var payment = await _context.PaymentEntries
                .FirstOrDefaultAsync(x => x.DocNo == docNo);

            if (payment == null)
                throw new Exception("Payment not found.");

            _context.PaymentEntries.Remove(payment);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
