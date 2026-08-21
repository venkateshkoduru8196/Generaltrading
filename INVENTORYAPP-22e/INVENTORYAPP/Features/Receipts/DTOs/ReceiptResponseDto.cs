namespace INVENTORYAPP.Features.Receipts.DTOs;

public class ReceiptResponseDto
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;

    public long DocNo { get; set; }
}