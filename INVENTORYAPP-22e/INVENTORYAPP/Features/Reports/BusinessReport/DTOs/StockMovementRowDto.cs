namespace INVENTORYAPP.Features.Reports.BusinessReport.DTOs;

public class StockMovementRowDto
{
    public string Metal { get; set; } = string.Empty;

    public string AccountName { get; set; } = string.Empty;

    public decimal Opening { get; set; }

    public decimal MoveIn { get; set; }

    public decimal MoveOut { get; set; }

    public decimal Closing { get; set; }
}