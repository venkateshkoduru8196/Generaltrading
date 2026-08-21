namespace INVENTORYAPP.Features.Masters.Accounts.DTOs;

public class AccountResponse
{
    public int Id { get; set; }

    //==========================================
    // Company
    //==========================================

    public int CompanyId { get; set; }

    //==========================================
    // Account
    //==========================================

    public string AccountCode { get; set; } = string.Empty;

    public string AccountName { get; set; } = string.Empty;

    //==========================================
    // Account Type
    //==========================================

    public string Actype { get; set; } = "G";

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