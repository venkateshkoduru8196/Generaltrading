namespace INVENTORYAPP.Features.Masters.Units.DTOs;

public class UpdateUnitRequest
{
    public int Id { get; set; }

    public string code { get; set; } = string.Empty;

    public string description { get; set; } = string.Empty;
}