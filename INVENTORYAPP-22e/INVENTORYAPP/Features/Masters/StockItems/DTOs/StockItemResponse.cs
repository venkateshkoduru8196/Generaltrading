namespace INVENTORYAPP.Features.Masters.StockItems.DTOs;

public class StockItemResponse
{
    public int Id { get; set; }

    //==========================================
    // Company
    //==========================================

    public int CompanyId { get; set; }

    //==========================================
    // Stock
    //==========================================

    public string StockCode { get; set; } = string.Empty;

    public string StockName { get; set; } = string.Empty;

    public decimal TaxRate { get; set; }

    //==========================================
    // Status
    //==========================================

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    //==========================================
    // Audit
    //==========================================

    public DateTime CreatedOn { get; set; }

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DeletedBy { get; set; }
}