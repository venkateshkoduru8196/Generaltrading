namespace INVENTORYAPP.Models;

public class DocumentSequence
{
    // ==========================================
    // Primary Key
    // ==========================================
    public int Id { get; set; }

    // ==========================================
    // Company
    // ==========================================

    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    // ==========================================
    // Module Code
    // ==========================================

    public string ModuleCode { get; set; } = string.Empty;

    // ==========================================
    // Prefix
    // ==========================================

    public string Prefix { get; set; } = string.Empty;

    // ==========================================
    // Financial Year
    // ==========================================

    public string FinancialYear { get; set; } = string.Empty;

    // ==========================================
    // Current Number
    // ==========================================

    public int CurrentNumber { get; set; }

    // ==========================================
    // Digits
    // ==========================================

    public int Digits { get; set; } = 6;

    // ==========================================
    // Separator
    // ==========================================

    public string Separator { get; set; } = string.Empty;

    // ==========================================
    // Active
    // ==========================================

    public bool IsActive { get; set; } = true;

    // ==========================================
    // Audit
    // ==========================================

    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    public DateTime? ModifiedOn { get; set; }
}