namespace INVENTORYAPP.Models;

public class ReceiptEntry
{
    //==========================================
    // Primary Key
    //==========================================

    public long DocNo { get; set; }

    //==========================================
    // Receipt
    //==========================================

    public DateTime DocDate { get; set; }

    // AccountMaster.Id
    // C = Customer
    // S = Supplier
    public int PartyId { get; set; }

    public DateTime STimestamp { get; set; }

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

    //==========================================
    // Navigation
    //==========================================

    // PartyMaster navigation removed.
    // PartyId refers to AccountMaster.Id.


    public ICollection<ReceiptEntryDetail> Details { get; set; }
        = new List<ReceiptEntryDetail>();
}