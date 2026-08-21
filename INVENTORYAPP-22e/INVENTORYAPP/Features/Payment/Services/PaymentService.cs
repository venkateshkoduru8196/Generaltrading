using INVENTORYAPP.Features.Masters.Accounts.DTOs;
using INVENTORYAPP.Features.Payments.DTOs;
using INVENTORYAPP.Features.Payments.Interface;

namespace INVENTORYAPP.Features.Payments.Services;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<List<PaymentPartyLookupResponse>> GetPartiesAsync()
        => await _paymentRepository.GetPartiesAsync();

    public async Task<List<AccountLookupResponse>> GetAccountsAsync()
        => await _paymentRepository.GetAccountsAsync();

    public async Task<long> GetNextPaymentNumberAsync()
        => await _paymentRepository.GetNextPaymentNumberAsync();

    public async Task<PaymentResponseDto> SavePaymentAsync(SavePaymentDto dto)
    {
        var docNo = await _paymentRepository.SavePaymentAsync(dto);

        return new PaymentResponseDto
        {
            Success = true,
            Message = "Payment saved successfully.",
            DocNo = docNo
        };
    }

    public async Task<List<PaymentSearchDto>> SearchAsync(string keyword)
        => await _paymentRepository.SearchAsync(keyword);

    public async Task<PaymentDto?> GetPaymentByDocNoAsync(long docNo)
        => await _paymentRepository.GetPaymentByDocNoAsync(docNo);

    public async Task DeletePaymentAsync(long docNo)
        => await _paymentRepository.DeletePaymentAsync(docNo);
}
