using INVENTORYAPP.Features.Authentication.DTOs.Auth;
using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Authentication.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterCustomerAsync(
        RegisterRequestDto dto);

    Task<AuthResponseDto> LoginAsync(
        LoginRequestDto dto);

    Task<AuthResponseDto> RefreshTokenAsync(
        string refreshToken);


    Task<bool> CreateUserAsync(CreateUserDto dto);

    Task SetSuperAdminEditPasswordAsync();
    //Task VerifyEditPasswordAsync(
    //ApplicationUser user,
    //string password);


    Task VerifyEditPasswordAsync(
    string userId,
    string password);


}


