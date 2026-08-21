using INVENTORYAPP.Features.UserManagement.DTOs;

namespace INVENTORYAPP.Features.UserManagement.Interfaces;

public interface IUserService
{
    //====================================================
    // ADMIN
    //====================================================

    Task<PagedUserResponseDto> GetAdminsAsync(
        UserListRequestDto request);

    Task<UserResponseDto?> GetAdminByIdAsync(
        string userId);

    Task UpdateAdminAsync(
        UpdateUserRequestDto request);

    Task UpdateAdminStatusAsync(
        UpdateUserStatusRequestDto request);

    Task DeleteAdminAsync(
        string userId);


    //====================================================
    // EMPLOYEE
    //====================================================

    Task<PagedUserResponseDto> GetEmployeesAsync(
        UserListRequestDto request);

    Task<UserResponseDto?> GetEmployeeByIdAsync(
        string userId);

    Task UpdateEmployeeAsync(
        UpdateUserRequestDto request);

    Task UpdateEmployeeStatusAsync(
        UpdateUserStatusRequestDto request);

    Task DeleteEmployeeAsync(
        string userId);
}