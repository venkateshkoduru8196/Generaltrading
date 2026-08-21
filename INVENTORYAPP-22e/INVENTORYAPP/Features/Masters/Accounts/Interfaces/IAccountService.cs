using INVENTORYAPP.Features.Masters.Accounts.DTOs;

namespace INVENTORYAPP.Features.Masters.Accounts.Interfaces;

public interface IAccountService
{
    Task<List<AccountResponse>> GetAllAsync();

    Task<AccountResponse?> GetByIdAsync(int id);

    Task<List<AccountLookupResponse>> GetLookupAsync();

    Task<AccountResponse> CreateAsync(CreateAccountRequest request);

    Task<AccountResponse> UpdateAsync(UpdateAccountRequest request);

    Task DeleteAsync(int id);
}