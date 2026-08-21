using INVENTORYAPP.Features.Masters.Accounts.DTOs;
using INVENTORYAPP.Features.Receipts.DTOs;

namespace INVENTORYAPP.Features.Receipts.Repositories;

public interface IReceiptRepository
{
    Task<List<ReceiptPartyLookupResponse>> GetPartiesAsync();

    Task<List<AccountLookupResponse>> GetAccountsAsync();

    Task<long> GetNextReceiptNumberAsync();
    Task<List<ReceiptSearchDto>> SearchAsync(string keyword);
    Task<long> SaveReceiptAsync(SaveReceiptDto dto);
    Task<ReceiptDto?> GetReceiptByDocNoAsync(long docNo);
    Task DeleteReceiptAsync(long docNo);
}
