namespace INVENTORYAPP.Features.Sales.Export.DTOs;

public class SalesInvoiceItemExportDto
{
    public int SlNo { get; set; }

    public string StockCode { get; set; } = string.Empty;

    public string StockName { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Unit { get; set; } = string.Empty;

    public decimal Qty { get; set; }

    public decimal Rate { get; set; }

    public decimal Amount { get; set; }

    public decimal TaxableAmount { get; set; }

    public decimal TaxRate { get; set; }

    public decimal TaxAmount { get; set; }
}