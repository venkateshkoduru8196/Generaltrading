using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using INVENTORYAPP.Infrastructure.Exceptions;
using INVENTORYAPP.Features.Authentication.Interfaces;
using INVENTORYAPP.Features.Authentication.DTOs.Auth;

namespace INVENTORYAPP.Features.Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(
        RegisterRequestDto dto)
    {
        var result =
            await _authService
                .RegisterCustomerAsync(dto);

        return Ok(result);
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login(
    LoginRequestDto dto)
    {
        var result =
            await _authService.LoginAsync(dto);

        return Ok(result);
    }




    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(
   RefreshTokenRequestDto dto)
    {
        var result =
            await _authService.RefreshTokenAsync(
                dto.RefreshToken);

        return Ok(result);
    }









    [Authorize(Roles = "SuperAdmin")]
    [HttpPost("create-admin")]
    public async Task<IActionResult> CreateAdmin(
    CreateUserDto dto)
    {
        dto.Role = "Admin";

        var result =
            await _authService.CreateUserAsync(
                dto);

        return Ok(new
        {
            Message = "Admin created successfully"
        });
    }



    [Authorize(Roles = "SuperAdmin,Admin")]
    [HttpPost("create-employee")]
    public async Task<IActionResult> CreateEmployee(
    CreateUserDto dto)
    {
        dto.Role = "Employee";

        var result =
            await _authService.CreateUserAsync(
                dto);

        return Ok(new
        {
            Message = "Employee created successfully"
        });
    }


    [Authorize(Roles = "SuperAdmin")]
    [HttpGet("claims")]
    public IActionResult Claims()
    {
        return Ok(
            User.Claims.Select(x => new
            {
                x.Type,
                x.Value
            }));
    }

    //[AllowAnonymous]
    //[HttpGet("test")]
    //public IActionResult Test()
    //{
    //    return Ok("API Working");
    //}


    //[AllowAnonymous]
    //[HttpGet("headers")]
    //public IActionResult Headers()
    //{
    //    return Ok(Request.Headers["Authorization"].ToString());
    //}



    [Authorize]
    [HttpGet("headers")]
    public IActionResult Headers()
    {
        return Ok(new
        {
            Header =
                Request.Headers["Authorization"]
                    .ToString(),

            Claims =
                User.Claims.Select(x => new
                {
                    x.Type,
                    x.Value
                })
        });
    }



    [Authorize]
    [HttpPost("verify-edit-password")]
    public async Task<IActionResult> VerifyEditPassword(
    VerifyEditPasswordRequestDto dto)
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        await _authService.VerifyEditPasswordAsync(
            userId,
            dto.Password);

        return Ok(new
        {
            Message = "Password verified successfully."
        });
    }

    [AllowAnonymous]
    [HttpPost("set-superadmin-edit-password")]
    public async Task<IActionResult> SetSuperAdminEditPassword()
    {
        await _authService.SetSuperAdminEditPasswordAsync();

        return Ok(new
        {
            Message = "SuperAdmin Edit Password set successfully."
        });
    }
}