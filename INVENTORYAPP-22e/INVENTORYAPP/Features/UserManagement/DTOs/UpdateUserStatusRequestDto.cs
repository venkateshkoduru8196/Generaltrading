namespace INVENTORYAPP.Features.UserManagement.DTOs;

public class UpdateUserStatusRequestDto
{
    public string UserId { get; set; } = string.Empty;

    public bool IsActive { get; set; }
}