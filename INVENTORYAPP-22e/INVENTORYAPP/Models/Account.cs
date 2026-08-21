namespace INVENTORYAPP.Models;

public class Account
{
    public int Id { get; set; }

    //==========================================
    // Company
    //==========================================

    public int CompanyId { get; set; }

    public Company? Company { get; set; }

    //==========================================
    // Account
    //==========================================

    public string AccountCode { get; set; } = string.Empty;

    public string AccountName { get; set; } = string.Empty;

    //==========================================
    // Account Type
    //
    // G = General
    // B = Bank/Cash
    // C = Customer
    // S = Supplier
    //==========================================

    public string Actype { get; set; } = "G";

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