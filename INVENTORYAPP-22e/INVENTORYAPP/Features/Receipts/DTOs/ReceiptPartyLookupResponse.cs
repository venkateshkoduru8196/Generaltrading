namespace INVENTORYAPP.Features.Receipts.DTOs;

public class ReceiptPartyLookupResponse
{
    public int Id { get; set; }

    public string PartyCode { get; set; } = string.Empty;

    public string PartyName { get; set; } = string.Empty;
}
