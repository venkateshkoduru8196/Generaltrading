namespace INVENTORYAPP.Features.Masters.StockItems.DTOs;

public class CreateStockItemRequest
{
    public string StockCode { get; set; } = string.Empty;

    public string StockName { get; set; } = string.Empty;

    public decimal TaxRate { get; set; }
}