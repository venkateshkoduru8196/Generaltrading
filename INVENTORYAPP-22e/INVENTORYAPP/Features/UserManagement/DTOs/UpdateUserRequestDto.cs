namespace INVENTORYAPP.Features.UserManagement.DTOs;

public class UpdateUserRequestDto
{
    //====================================================
    // USER
    //====================================================

    public string UserId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;


    //====================================================
    // COMPANY
    //====================================================

    public int? CompanyId { get; set; }
}