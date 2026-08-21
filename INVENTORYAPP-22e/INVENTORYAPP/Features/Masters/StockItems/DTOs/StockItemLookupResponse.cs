namespace INVENTORYAPP.Features.Masters.StockItems.DTOs;

public class StockItemLookupResponse
{
    public int Id { get; set; }

    public string StockCode { get; set; } = string.Empty;

    public string StockName { get; set; } = string.Empty;

    public decimal TaxRate { get; set; }
}