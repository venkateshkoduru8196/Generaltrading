namespace INVENTORYAPP.Features.Reports.BusinessReport.DTOs;

public class BusinessReportRequestDto
{
    public string ReportType { get; set; } = string.Empty;

    // Daily
    public DateTime? ReportDate { get; set; }

    // Monthly
    public int? Month { get; set; }
    public int? Year { get; set; }

    // Periodical
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}