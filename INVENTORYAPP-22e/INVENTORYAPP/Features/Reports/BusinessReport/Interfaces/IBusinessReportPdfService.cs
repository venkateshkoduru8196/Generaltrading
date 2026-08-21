using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;

public interface IBusinessReportPdfService
{
    Task<byte[]> GeneratePdfAsync(BusinessReportRequestDto request);
}