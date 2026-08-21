namespace INVENTORYAPP.Features.Masters.Parties.DTOs;

public class CreatePartyRequest
{
    public string PartyCode { get; set; } = string.Empty;

    public string PartyName { get; set; } = string.Empty;
}