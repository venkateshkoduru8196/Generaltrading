namespace INVENTORYAPP.Features.Sales.DTOs;
public class GSalCreateRequestDto 
{ public DateTime DocDate { get; set; }
    public string PartyCode { get; set; } = string.Empty;
    public List<GSalDetailRequestDto> Details { get; set; } = new();
}