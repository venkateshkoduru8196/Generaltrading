namespace INVENTORYAPP.Features.Payments.DTOs;

public class PaymentSearchDto
{
    public long DocNo { get; set; }

    public DateTime DocDate { get; set; }

    public string PartyCode { get; set; } = string.Empty;

    public string? PartyName { get; set; }
}