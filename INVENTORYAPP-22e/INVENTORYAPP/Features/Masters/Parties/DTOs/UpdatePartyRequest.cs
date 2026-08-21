namespace INVENTORYAPP.Features.Masters.Parties.DTOs;

public class UpdatePartyRequest
{
    public int Id { get; set; }

    public string PartyCode { get; set; } = string.Empty;

    public string PartyName { get; set; } = string.Empty;
}