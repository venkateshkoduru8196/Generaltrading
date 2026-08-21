using INVENTORYAPP.Features.UserManagement.DTOs;
using INVENTORYAPP.Features.UserManagement.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.UserManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _service;

    public UserController(
        IUserService service)
    {
        _service = service;
    }


    //====================================================
    // ADMIN
    //====================================================

    [HttpGet("admins")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> GetAdmins(
        [FromQuery] UserListRequestDto request)
    {
        var result =
            await _service.GetAdminsAsync(request);

        return Ok(result);
    }


    [HttpGet("admins/{id}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> GetAdminById(
        string id)
    {
        var result =
            await _service.GetAdminByIdAsync(id);

        if (result == null)
        {
            return NotFound(new
            {
                Message = "Admin not found."
            });
        }

        return Ok(result);
    }


    [HttpPut("admins/{id}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> UpdateAdmin(
        string id,
        [FromBody] UpdateUserRequestDto request)
    {
        request.UserId = id;

        await _service.UpdateAdminAsync(request);

        return Ok(new
        {
            Message = "Admin updated successfully."
        });
    }


    [HttpPatch("admins/{id}/status")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> UpdateAdminStatus(
        string id,
        [FromBody] UpdateUserStatusRequestDto request)
    {
        request.UserId = id;

        await _service.UpdateAdminStatusAsync(request);

        return Ok(new
        {
            Message = request.IsActive
                ? "Admin activated successfully."
                : "Admin deactivated successfully."
        });
    }


    [HttpDelete("admins/{id}")]
    [Authorize(Roles = "SuperAdmin")]
    public async Task<IActionResult> DeleteAdmin(
        string id)
    {
        await _service.DeleteAdminAsync(id);

        return Ok(new
        {
            Message = "Admin deleted successfully."
        });
    }


    //====================================================
    // EMPLOYEE
    //====================================================

    [HttpGet("employees")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> GetEmployees(
        [FromQuery] UserListRequestDto request)
    {
        var result =
            await _service.GetEmployeesAsync(request);

        return Ok(result);
    }


    [HttpGet("employees/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> GetEmployeeById(
        string id)
    {
        var result =
            await _service.GetEmployeeByIdAsync(id);

        if (result == null)
        {
            return NotFound(new
            {
                Message = "Employee not found."
            });
        }

        return Ok(result);
    }


    [HttpPut("employees/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> UpdateEmployee(
        string id,
        [FromBody] UpdateUserRequestDto request)
    {
        request.UserId = id;

        await _service.UpdateEmployeeAsync(request);

        return Ok(new
        {
            Message = "Employee updated successfully."
        });
    }


    [HttpPatch("employees/{id}/status")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> UpdateEmployeeStatus(
        string id,
        [FromBody] UpdateUserStatusRequestDto request)
    {
        request.UserId = id;

        await _service.UpdateEmployeeStatusAsync(request);

        return Ok(new
        {
            Message = request.IsActive
                ? "Employee activated successfully."
                : "Employee deactivated successfully."
        });
    }


    [HttpDelete("employees/{id}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> DeleteEmployee(
        string id)
    {
        await _service.DeleteEmployeeAsync(id);

        return Ok(new
        {
            Message = "Employee deleted successfully."
        });
    }
}