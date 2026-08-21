namespace INVENTORYAPP.Features.Receipts.DTOs;

public class ReceiptDto
{
    public long DocNo { get; set; }

    public DateTime DocDate { get; set; }

    public int PartyId { get; set; }

    public List<ReceiptDetailDto> Details { get; set; } = new();
}