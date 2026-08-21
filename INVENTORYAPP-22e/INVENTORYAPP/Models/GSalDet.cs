namespace INVENTORYAPP.Models;

public class GSalDet
{
    public int Id { get; set; }

    // ==========================================
    // Company (Multi-Tenant)
    // ==========================================

    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    // ==========================================
    // Document
    // ==========================================

    public string docno { get; set; } = string.Empty;

    public DateTime docdate { get; set; }

    public DateTime stimestamp { get; set; }

    public string partycode { get; set; } = string.Empty;

    // ==========================================
    // Item
    // ==========================================

    public int slno { get; set; }

    public string stkcode { get; set; } = string.Empty;

    public string stkname { get; set; } = string.Empty;

    public string description { get; set; } = string.Empty;

    public string unitcode { get; set; } = string.Empty;

    public string unitname { get; set; } = string.Empty;

    // ==========================================
    // Quantity
    // ==========================================

    public decimal qty { get; set; }

    public decimal rate { get; set; }

    public decimal amount { get; set; }

    public decimal taxableamt { get; set; }

    public decimal taxrate { get; set; }

    public decimal taxamt { get; set; }

    // ==========================================
    // Audit
    // ==========================================

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public string CreatedBy { get; set; } = string.Empty;

    public DateTime? ModifiedOn { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public string? DeletedBy { get; set; }

    // ==========================================
    // Navigation
    // ==========================================

    public int GSalId { get; set; }

    public GSal? GSal { get; set; }
}