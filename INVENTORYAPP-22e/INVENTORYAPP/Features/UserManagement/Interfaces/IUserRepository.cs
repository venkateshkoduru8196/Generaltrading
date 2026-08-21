using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.UserManagement.Interfaces;

public interface IUserRepository
{
    //====================================================
    // GET USERS
    //====================================================

    Task<(List<ApplicationUser> Users, int TotalRecords)>
        GetUsersAsync(
            string roleName,
            int? companyId,
            string? search,
            bool? isActive,
            int pageNumber,
            int pageSize);


    //====================================================
    // GET USER BY ID
    //====================================================

    Task<ApplicationUser?> GetByIdAsync(
        string userId);


    //====================================================
    // GET USER ROLE
    //====================================================

    Task<(string? RoleId, string? RoleName)>
        GetRoleAsync(
            ApplicationUser user);


    //====================================================
    // UPDATE USER
    //====================================================

    Task UpdateAsync(
        ApplicationUser user);


    //====================================================
    // UPDATE USER STATUS
    //====================================================

    Task UpdateStatusAsync(
        ApplicationUser user,
        bool isActive);


    //====================================================
    // SOFT DELETE USER
    //====================================================

    Task SoftDeleteAsync(
        ApplicationUser user,
        string deletedBy);


    //====================================================
    // SAVE CHANGES
    //====================================================

    Task SaveChangesAsync();
}