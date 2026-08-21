namespace INVENTORYAPP.Features.Reports.BusinessReport.DTOs;

public class BusinessReportResponseDto
{
    public string CompanyName { get; set; } = string.Empty;

    public string CompanyAddress { get; set; } = string.Empty;

    public DateTime ReportDateTime { get; set; }

    public List<StockMovementRowDto> StockMovements { get; set; } = new();
}