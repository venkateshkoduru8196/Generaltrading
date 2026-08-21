namespace INVENTORYAPP.Features.Authentication.DTOs.Auth;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public string UserId { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string RoleId { get; set; } = string.Empty;

    public string RoleName { get; set; } = string.Empty;
}