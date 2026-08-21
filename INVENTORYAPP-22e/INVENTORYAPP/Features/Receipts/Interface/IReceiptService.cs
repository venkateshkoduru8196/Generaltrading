using INVENTORYAPP.Features.Masters.Accounts.DTOs;
using INVENTORYAPP.Features.Receipts.DTOs;

namespace INVENTORYAPP.Features.Receipts.Interface;

public interface IReceiptService
{
    Task<List<ReceiptPartyLookupResponse>> GetPartiesAsync();
    Task<List<AccountLookupResponse>> GetAccountsAsync();
    Task<long> GetNextReceiptNumberAsync();
    Task<ReceiptResponseDto> SaveReceiptAsync(SaveReceiptDto dto);
    Task<List<ReceiptSearchDto>> SearchAsync(string keyword);
    Task<ReceiptDto?> GetReceiptByDocNoAsync(long docNo);
    Task DeleteReceiptAsync(long docNo);
}
