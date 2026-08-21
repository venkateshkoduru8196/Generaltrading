namespace INVENTORYAPP.Features.Receipts.DTOs;

public class ReceiptSearchDto
{
    public long DocNo { get; set; }

    public DateTime DocDate { get; set; }

    public string PartyCode { get; set; } = string.Empty;

    public string? PartyName { get; set; }
}