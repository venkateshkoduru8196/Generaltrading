using System.ComponentModel.DataAnnotations;

namespace INVENTORYAPP.Features.Receipts.DTOs;

public class SaveReceiptDto
{
    public long DocNo { get; set; }

    [Required]
    public DateTime DocDate { get; set; }

    [Required]
    public int PartyId { get; set; }

    public List<ReceiptDetailDto> Details { get; set; } = new();
}