using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;

public interface IBusinessReportRepository
{
    Task<BusinessReportResponseDto> GetBusinessReportAsync(
        BusinessReportRequestDto request);
}