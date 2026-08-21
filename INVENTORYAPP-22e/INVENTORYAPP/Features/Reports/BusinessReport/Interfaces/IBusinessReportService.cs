using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;

public interface IBusinessReportService
{
    Task<BusinessReportResponseDto> GetBusinessReportAsync(
        BusinessReportRequestDto request);
}


