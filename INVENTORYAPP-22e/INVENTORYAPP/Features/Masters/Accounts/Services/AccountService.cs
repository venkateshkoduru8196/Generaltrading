using INVENTORYAPP.Features.Masters.Accounts.DTOs;
using INVENTORYAPP.Features.Masters.Accounts.Interfaces;
using INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;
using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.Accounts.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public AccountService(
        IAccountRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    //==========================================
    // Get All
    //==========================================

    public async Task<List<AccountResponse>> GetAllAsync()
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var accounts =
            await _repository.GetAllAsync(companyId);

        return accounts
            .Select(MapToResponse)
            .ToList();
    }

    //==========================================
    // Get By Id
    //==========================================

    public async Task<AccountResponse?> GetByIdAsync(int id)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var account =
            await _repository.GetByIdAsync(
                companyId,
                id);

        if (account == null)
            return null;

        return MapToResponse(account);
    }

    //==========================================
    // Lookup
    //==========================================

    public async Task<List<AccountLookupResponse>> GetLookupAsync()
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var accounts =
            await _repository.GetAllAsync(companyId);

        return accounts
            .Select(x => new AccountLookupResponse
            {
                Id = x.Id,

                AccountCode = x.AccountCode,

                AccountName = x.AccountName,

                Actype = x.Actype
            })
            .ToList();
    }

    //==========================================
    // Create
    //==========================================

    public async Task<AccountResponse> CreateAsync(
        CreateAccountRequest request)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        //==========================================
        // Validate Account Type
        //==========================================

        string actype =
            NormalizeAccountType(request.Actype);

        //==========================================
        // Check Duplicate Account Code
        //==========================================

        if (await _repository.ExistsAsync(
            companyId,
            request.AccountCode))
        {
            throw new Exception(
                "Account Code already exists.");
        }

        //==========================================
        // Create Account
        //==========================================

        var account = new Account
        {
            CompanyId = companyId,

            AccountCode = request.AccountCode,

            AccountName = request.AccountName,

            Actype = actype,

            IsActive = true,

            IsDeleted = false,

            CreatedOn = DateTime.UtcNow,

            CreatedBy = _currentUser.UserName
        };

        await _repository.AddAsync(account);

        await _repository.SaveChangesAsync();

        return MapToResponse(account);
    }

    //==========================================
    // Update
    //==========================================

    public async Task<AccountResponse> UpdateAsync(
        UpdateAccountRequest request)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        //==========================================
        // Get Existing Account
        //==========================================

        var account =
            await _repository.GetByIdAsync(
                companyId,
                request.Id);

        if (account == null)
            throw new Exception("Account not found.");

        //==========================================
        // Validate Account Type
        //==========================================

        string actype =
            NormalizeAccountType(request.Actype);

        //==========================================
        // Check Duplicate Account Code
        //==========================================

        var existing =
            await _repository.GetByCodeAsync(
                companyId,
                request.AccountCode);

        if (existing != null &&
            existing.Id != request.Id)
        {
            throw new Exception(
                "Account Code already exists.");
        }

        //==========================================
        // Update Account
        //==========================================

        account.AccountCode =
            request.AccountCode;

        account.AccountName =
            request.AccountName;

        account.Actype =
            actype;

        account.ModifiedOn =
            DateTime.UtcNow;

        account.ModifiedBy =
            _currentUser.UserName;

        await _repository.UpdateAsync(account);

        await _repository.SaveChangesAsync();

        return MapToResponse(account);
    }

    //==========================================
    // Delete
    //==========================================

    public async Task DeleteAsync(int id)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var account =
            await _repository.GetByIdAsync(
                companyId,
                id);

        if (account == null)
            throw new Exception("Account not found.");

        account.DeletedOn =
            DateTime.UtcNow;

        account.DeletedBy =
            _currentUser.UserName;

        await _repository.DeleteAsync(account);

        await _repository.SaveChangesAsync();
    }

    //==========================================
    // Normalize / Validate Account Type
    //==========================================

    private static string NormalizeAccountType(
        string? actype)
    {
        string value =
            (actype ?? string.Empty)
            .Trim()
            .ToUpperInvariant();

        if (value is not ("G" or "B" or "C" or "S"))
        {
            throw new Exception(
                "Invalid Account Type. " +
                "Allowed values are G, B, C, or S.");
        }

        return value;
    }

    //==========================================
    // Map Entity To Response
    //==========================================

    private static AccountResponse MapToResponse(
        Account account)
    {
        return new AccountResponse
        {
            Id = account.Id,

            CompanyId = account.CompanyId,

            AccountCode = account.AccountCode,

            AccountName = account.AccountName,

            Actype = account.Actype,

            IsActive = account.IsActive,

            IsDeleted = account.IsDeleted,

            CreatedOn = account.CreatedOn,

            CreatedBy = account.CreatedBy,

            ModifiedOn = account.ModifiedOn,

            ModifiedBy = account.ModifiedBy,

            DeletedOn = account.DeletedOn,

            DeletedBy = account.DeletedBy
        };
    }
}




