namespace INVENTORYAPP.Models;

public class Party
{
    //==========================================
    // Primary Key
    //==========================================

    public int Id { get; set; }

    //==========================================
    // Company
    //==========================================

    public int CompanyId { get; set; }


    public Company? Company { get; set; }

    //==========================================
    // Party
    //==========================================

    public string PartyCode { get; set; } = string.Empty;

    public string PartyName { get; set; } = string.Empty;

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