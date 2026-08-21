namespace INVENTORYAPP.Models;

public class GSal
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

    // ==========================================
    // Party
    // ==========================================

    public string partycode { get; set; } = string.Empty;

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

    public ICollection<GSalDet> Details { get; set; }
        = new List<GSalDet>();
}