using INVENTORYAPP.Data;
using INVENTORYAPP.Features.Masters.Accounts.DTOs;
using INVENTORYAPP.Features.Receipts.DTOs;
using INVENTORYAPP.Features.Receipts.Repositories;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Receipts.Repositories;

public class ReceiptRepository : IReceiptRepository
{
    private readonly AppDbContext _context;

    public ReceiptRepository(AppDbContext context)
    {
        _context = context;
    }

    //==========================================
    // Party Lookup
    // AccountMaster: C = Customer, S = Supplier
    //==========================================

    public async Task<List<ReceiptPartyLookupResponse>> GetPartiesAsync()
    {
        return await _context.Accounts
            .Where(x => x.IsActive && !x.IsDeleted &&
                        (x.Actype == "C" || x.Actype == "S"))
            .OrderBy(x => x.AccountName)
            .Select(x => new ReceiptPartyLookupResponse
            {
                Id = x.Id,
                PartyCode = x.AccountCode,
                PartyName = x.AccountName
            })
            .ToListAsync();
    }

    //==========================================
    // Account Lookup
    // AccountMaster: B = Bank/Cash
    //==========================================

    public async Task<List<AccountLookupResponse>> GetAccountsAsync()
    {
        return await _context.Accounts
            .Where(x => x.IsActive &&
                        !x.IsDeleted &&
                        x.Actype == "B")
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
    //==========================================
    // Save Receipt
    //==========================================

    public async Task<long> SaveReceiptAsync(SaveReceiptDto dto)
    {
        using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            //==========================================
            // Validate Party from AccountMaster
            // C = Customer, S = Supplier
            //==========================================

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

            //==========================================
            // Find Existing Receipt
            //==========================================

            ReceiptEntry? receipt = await _context.ReceiptEntries
                .FirstOrDefaultAsync(x => x.DocNo == dto.DocNo);

            if (receipt != null)
            {
                //==========================================
                // UPDATE RECEIPT
                //==========================================

                receipt.DocDate = dto.DocDate;
                receipt.PartyId = dto.PartyId;
                receipt.STimestamp = DateTime.Now;
                receipt.ModifiedOn = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                var oldDetails = await _context.ReceiptEntryDetails
                    .Where(x => x.DocNo == receipt.DocNo)
                    .ToListAsync();

                _context.ReceiptEntryDetails.RemoveRange(oldDetails);

                await _context.SaveChangesAsync();
            }
            else
            {
                //==========================================
                // INSERT NEW RECEIPT
                //==========================================

                receipt = new ReceiptEntry
                {
                    DocDate = dto.DocDate,
                    PartyId = dto.PartyId,
                    STimestamp = DateTime.Now
                };

                _context.ReceiptEntries.Add(receipt);

                await _context.SaveChangesAsync();
            }

            //==========================================
            // Receipt Details
            //==========================================

            int slNo = 1;

            foreach (var item in dto.Details)
            {
                //==========================================
                // Validate Account from AccountMaster
                // B = Bank/Cash
                //==========================================

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

                var detail = new ReceiptEntryDetail
                {
                    DocNo = receipt.DocNo,
                    DocDate = dto.DocDate,
                    PartyId = dto.PartyId,
                    STimestamp = DateTime.Now,
                    SlNo = slNo++,
                    AccountId = item.AccountId,
                    AcName = account.AccountName,
                    Amount = item.Amount
                };

                _context.ReceiptEntryDetails.Add(detail);
            }

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return receipt.DocNo;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    //==========================================
    // Next Receipt Number
    //==========================================

    public async Task<long> GetNextReceiptNumberAsync()
    {
        var lastDocNo = await _context.ReceiptEntries
            .OrderByDescending(x => x.DocNo)
            .Select(x => (long?)x.DocNo)
            .FirstOrDefaultAsync();

        return (lastDocNo ?? 0) + 1;
    }

    //==========================================
    // Search
    //==========================================

    public async Task<List<ReceiptSearchDto>> SearchAsync(string keyword)
    {
        keyword = keyword?.Trim().ToLower() ?? string.Empty;

        var result = await (
            from receipt in _context.ReceiptEntries
            join account in _context.Accounts
                on receipt.PartyId equals account.Id
            where
                receipt.DocNo.ToString().Contains(keyword)
                || account.AccountCode.ToLower().Contains(keyword)
                || account.AccountName.ToLower().Contains(keyword)
                || receipt.DocDate.ToString().Contains(keyword)
            select new ReceiptSearchDto
            {
                DocNo = receipt.DocNo,
                DocDate = receipt.DocDate,
                PartyCode = account.AccountCode,
                PartyName = account.AccountName
            })
            .Distinct()
            .OrderByDescending(x => x.DocNo)
            .ToListAsync();

        return result;
    }

    //==========================================
    // Get Receipt
    //==========================================

    public async Task<ReceiptDto?> GetReceiptByDocNoAsync(long docNo)
    {
        var receipt = await _context.ReceiptEntries
            .FirstOrDefaultAsync(x => x.DocNo == docNo);

        if (receipt == null)
            return null;

        var details = await _context.ReceiptEntryDetails
            .Where(x => x.DocNo == docNo)
            .OrderBy(x => x.SlNo)
            .Select(x => new ReceiptDetailDto
            {
                AccountId = x.AccountId,
                Amount = x.Amount
            })
            .ToListAsync();

        return new ReceiptDto
        {
            DocNo = receipt.DocNo,
            DocDate = receipt.DocDate,
            PartyId = receipt.PartyId,
            Details = details
        };
    }

    //==========================================
    // Delete Receipt
    //==========================================

    public async Task DeleteReceiptAsync(long docNo)
    {
        using var transaction =
            await _context.Database.BeginTransactionAsync();

        try
        {
            var receipt = await _context.ReceiptEntries
                .FirstOrDefaultAsync(x => x.DocNo == docNo);

            if (receipt == null)
            {
                throw new Exception("Receipt not found.");
            }

            _context.ReceiptEntries.Remove(receipt);

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
