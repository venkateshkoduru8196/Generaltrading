namespace INVENTORYAPP.Models;

public class StockItem
{
    public int Id { get; set; }

    //==========================================
    // Company
    //==========================================

    public int CompanyId { get; set; }

    public Company? Company { get; set; }

    //==========================================
    // Stock Details
    //==========================================

    public string StockCode { get; set; } = string.Empty;

    public string StockName { get; set; } = string.Empty;

    public decimal TaxRate { get; set; }

    //==========================================
    // Status
    //==========================================

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;

    //==========================================
    // Audit
    //==========================================

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DeletedBy { get; set; }
}