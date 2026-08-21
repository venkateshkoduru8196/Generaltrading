namespace INVENTORYAPP.Features.Authentication.DTOs.Auth;

public class CreateUserDto
{
    public string FullName { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;

    public string EditPassword { get; set; } = string.Empty;

    public string ConfirmEditPassword { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public int? CompanyId { get; set; }
}