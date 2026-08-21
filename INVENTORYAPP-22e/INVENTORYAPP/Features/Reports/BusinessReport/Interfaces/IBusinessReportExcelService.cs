using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;

public interface IBusinessReportExcelService
{
    Task<byte[]> GenerateExcelAsync(
        BusinessReportRequestDto request);
}