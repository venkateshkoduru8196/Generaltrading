namespace INVENTORYAPP.Features.Payments.DTOs;

public class PaymentResponseDto
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public long DocNo { get; set; }
}