using INVENTORYAPP.Features.Masters.Accounts.DTOs;
using INVENTORYAPP.Features.Payments.DTOs;

namespace INVENTORYAPP.Features.Payments.Interface;

public interface IPaymentRepository
{
    Task<List<PaymentPartyLookupResponse>> GetPartiesAsync();
    Task<List<AccountLookupResponse>> GetAccountsAsync();
    Task<long> GetNextPaymentNumberAsync();
    Task<List<PaymentSearchDto>> SearchAsync(string keyword);
    Task<long> SavePaymentAsync(SavePaymentDto dto);
    Task<PaymentDto?> GetPaymentByDocNoAsync(long docNo);
    Task DeletePaymentAsync(long docNo);
}
