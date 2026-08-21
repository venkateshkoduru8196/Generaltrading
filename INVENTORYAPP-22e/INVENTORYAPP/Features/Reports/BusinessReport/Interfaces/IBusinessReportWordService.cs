using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
namespace INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;

public interface IBusinessReportWordService
{
    Task<byte[]> GenerateWordAsync(
        BusinessReportRequestDto request);
}