namespace INVENTORYAPP.Models;

public class PaymentEntryDetail
{
    //==========================================
    // Primary Key
    //==========================================

    public long Id { get; set; }

    //==========================================
    // Foreign Key
    //==========================================

    public long DocNo { get; set; }

    public PaymentEntry? PaymentEntry { get; set; }

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

    // AccountId refers to AccountMaster.Id.
    // Only B (Bank/Cash) accounts are allowed
    // by Payment business logic.

    public Account? Account { get; set; }
}