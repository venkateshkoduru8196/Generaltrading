namespace INVENTORYAPP.Features.Sales.Export.DTOs;

public class SalesInvoiceExportDto
{
    //====================================================
    // Company Information
    //====================================================

    public string CompanyName { get; set; } = string.Empty;

    public string CompanyAddress { get; set; } = string.Empty;

    public string CompanyPhone { get; set; } = string.Empty;

    public string CompanyEmail { get; set; } = string.Empty;

    public string CompanyGSTIN { get; set; } = string.Empty;

    //====================================================
    // Invoice Information
    //====================================================

    public int SaleId { get; set; }

    public string InvoiceNo { get; set; } = string.Empty;

    public DateTime InvoiceDate { get; set; }

    //====================================================
    // Customer
    //====================================================

    public string PartyCode { get; set; } = string.Empty;

    public string PartyName { get; set; } = string.Empty;

    //====================================================
    // Totals
    //====================================================

    public decimal TotalQty { get; set; }

    public decimal TotalAmount { get; set; }

    public decimal TotalTax { get; set; }

    public decimal GrandTotal { get; set; }

    //====================================================
    // Audit
    //====================================================

    public DateTime GeneratedOn { get; set; }

    public string GeneratedBy { get; set; } = string.Empty;

    //====================================================
    // Invoice Items
    //====================================================

    public List<SalesInvoiceItemExportDto> Items { get; set; }
        = new();
}