namespace INVENTORYAPP.Features.Payments.DTOs;

public class PaymentDto
{
    public long DocNo { get; set; }

    public DateTime DocDate { get; set; }

    public int PartyId { get; set; }

    public List<PaymentDetailDto> Details { get; set; } = new();
}