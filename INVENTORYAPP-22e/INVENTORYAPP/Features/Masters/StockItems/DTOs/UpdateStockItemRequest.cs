namespace INVENTORYAPP.Features.Masters.StockItems.DTOs;

public class UpdateStockItemRequest
{
    public int Id { get; set; }

    public string StockCode { get; set; } = string.Empty;

    public string StockName { get; set; } = string.Empty;

    public decimal TaxRate { get; set; }

    public bool IsActive { get; set; }
}