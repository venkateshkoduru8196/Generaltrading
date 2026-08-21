using System.ComponentModel.DataAnnotations;

namespace INVENTORYAPP.Features.Payments.DTOs;

public class SavePaymentDto
{
    public long DocNo { get; set; }

    [Required]
    public DateTime DocDate { get; set; }

    [Required]
    public int PartyId { get; set; }

    public List<PaymentDetailDto> Details { get; set; } = new();
}