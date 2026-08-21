using INVENTORYAPP.Features.Masters.Accounts.DTOs;
using INVENTORYAPP.Features.Receipts.DTOs;
using INVENTORYAPP.Features.Receipts.Interface;
using INVENTORYAPP.Features.Receipts.Repositories;

namespace INVENTORYAPP.Features.Receipts.Services;

public class ReceiptService : IReceiptService
{
    private readonly IReceiptRepository _receiptRepository;

    public ReceiptService(IReceiptRepository receiptRepository)
    {
        _receiptRepository = receiptRepository;
    }

    public async Task<List<ReceiptPartyLookupResponse>> GetPartiesAsync()
    {
        return await _receiptRepository.GetPartiesAsync();
    }

    public async Task<List<AccountLookupResponse>> GetAccountsAsync()
    {
        return await _receiptRepository.GetAccountsAsync();
    }

    public async Task<long> GetNextReceiptNumberAsync()
    {
        return await _receiptRepository.GetNextReceiptNumberAsync();
    }

    public async Task<ReceiptResponseDto> SaveReceiptAsync(SaveReceiptDto dto)
    {
        var docNo = await _receiptRepository.SaveReceiptAsync(dto);

        return new ReceiptResponseDto
        {
            Success = true,
            Message = "Receipt saved successfully.",
            DocNo = docNo
        };
    }

    public async Task<List<ReceiptSearchDto>> SearchAsync(string keyword)
    {
        return await _receiptRepository.SearchAsync(keyword);
    }

    public async Task<ReceiptDto?> GetReceiptByDocNoAsync(long docNo)
    {
        return await _receiptRepository.GetReceiptByDocNoAsync(docNo);
    }

    public async Task DeleteReceiptAsync(long docNo)
    {
        await _receiptRepository.DeleteReceiptAsync(docNo);
    }
}
