using INVENTORYAPP.Features.Masters.Accounts.DTOs;
using INVENTORYAPP.Features.Payments.DTOs;

namespace INVENTORYAPP.Features.Payments.Interface;

public interface IPaymentService
{
    Task<List<PaymentPartyLookupResponse>> GetPartiesAsync();
    Task<List<AccountLookupResponse>> GetAccountsAsync();
    Task<long> GetNextPaymentNumberAsync();
    Task<PaymentResponseDto> SavePaymentAsync(SavePaymentDto dto);
    Task<List<PaymentSearchDto>> SearchAsync(string keyword);
    Task<PaymentDto?> GetPaymentByDocNoAsync(long docNo);
    Task DeletePaymentAsync(long docNo);
}
