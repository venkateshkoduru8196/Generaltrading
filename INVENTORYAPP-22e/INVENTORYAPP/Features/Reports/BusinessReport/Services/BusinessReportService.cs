using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Services;

public class BusinessReportService : IBusinessReportService
{
    private readonly IBusinessReportRepository _repository;

    public BusinessReportService(
        IBusinessReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<BusinessReportResponseDto> GetBusinessReportAsync(
        BusinessReportRequestDto request)
    {
        return await _repository.GetBusinessReportAsync(request);
    }
}