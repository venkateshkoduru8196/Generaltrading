namespace INVENTORYAPP.Models;

public class ReceiptEntryDetail
{
    //==========================================
    // Primary Key
    //==========================================

    public long Id { get; set; }

    //==========================================
    // Receipt Foreign Key
    //==========================================

    public long DocNo { get; set; }

    public ReceiptEntry? ReceiptEntry { get; set; }

    //==========================================
    // Detail
    //==========================================

    public DateTime DocDate { get; set; }

    // AccountMaster.Id
    // C = Customer
    // S = Supplier
    public int PartyId { get; set; }

    public DateTime STimestamp { get; set; }

    public int SlNo { get; set; }

    // AccountMaster.Id
    // B = Bank/Cash
    public int AccountId { get; set; }

    public string AcName { get; set; } = string.Empty;

    public decimal Amount { get; set; }

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

    // AccountMaster navigation.
    // AccountId refers to AccountMaster.Id.

   

    public Account? Account { get; set; }
}