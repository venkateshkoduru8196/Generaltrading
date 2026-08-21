namespace INVENTORYAPP.Features.Payments.DTOs;

public class PaymentPartyLookupResponse
{
    public int Id { get; set; }

    public string PartyCode { get; set; } = string.Empty;

    public string PartyName { get; set; } = string.Empty;
}
