using INVENTORYAPP.Data;
using INVENTORYAPP.Features.UserManagement.Interfaces;
using INVENTORYAPP.Models;

using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.UserManagement.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(
        AppDbContext context)
    {
        _context = context;
    }


    //====================================================
    // GET USERS
    //====================================================

    public async Task<(List<ApplicationUser> Users, int TotalRecords)>
        GetUsersAsync(
            string roleName,
            int? companyId,
            string? search,
            bool? isActive,
            int pageNumber,
            int pageSize)
    {
        var query =
            from user in _context.Users
            join userRoleMapping in _context.UserRoles
                on user.Id equals userRoleMapping.UserId
            join roleEntity in _context.Roles
                on userRoleMapping.RoleId equals roleEntity.Id
            where roleEntity.Name == roleName
                  && !user.IsDeleted
            select user;

        //================================================
        // COMPANY FILTER
        //================================================

        if (companyId.HasValue)
        {
            query = query.Where(x =>
                x.CompanyId == companyId.Value);
        }


        //================================================
        // ACTIVE FILTER
        //================================================

        if (isActive.HasValue)
        {
            query = query.Where(x =>
                x.IsActive == isActive.Value);
        }


        //================================================
        // SEARCH
        //================================================

        if (!string.IsNullOrWhiteSpace(search))
        {
            search = search.Trim();

            query = query.Where(x =>
                x.FullName.Contains(search) ||
                (x.UserName != null &&
                 x.UserName.Contains(search)) ||
                (x.Email != null &&
                 x.Email.Contains(search)) ||
                (x.PhoneNumber != null &&
                 x.PhoneNumber.Contains(search)));
        }


        //================================================
        // TOTAL RECORDS
        //================================================

        var totalRecords =
            await query.CountAsync();


        //================================================
        // PAGINATION
        //================================================

        pageNumber =
            pageNumber < 1
                ? 1
                : pageNumber;

        pageSize =
            pageSize < 1
                ? 20
                : pageSize;

        var users =
            await query
                .Include(x => x.Company)
                .OrderBy(x => x.FullName)
                .ThenBy(x => x.UserName)
                .Skip(
                    (pageNumber - 1) *
                    pageSize)
                .Take(pageSize)
                .ToListAsync();

        return (
            users,
            totalRecords
        );
    }


    //====================================================
    // GET USER BY ID
    //====================================================

    public async Task<ApplicationUser?> GetByIdAsync(
        string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            return null;

        return await _context.Users
            .Include(x => x.Company)
            .FirstOrDefaultAsync(x =>
                x.Id == userId &&
                !x.IsDeleted);
    }


    //====================================================
    // GET USER ROLE
    //====================================================

    public async Task<(string? RoleId, string? RoleName)>
        GetRoleAsync(
            ApplicationUser user)
    {
        var userRole =
            await (
                from userRoleMapping in _context.UserRoles
                join roleEntity in _context.Roles
                    on userRoleMapping.RoleId equals roleEntity.Id
                where userRoleMapping.UserId == user.Id
                select new
                {
                    RoleId = roleEntity.Id,
                    RoleName = roleEntity.Name
                })
            .FirstOrDefaultAsync();

        if (userRole == null)
        {
            return (null, null);
        }

        return (
            userRole.RoleId,
            userRole.RoleName
        );
    }


    //====================================================
    // UPDATE USER
    //====================================================

    public Task UpdateAsync(
        ApplicationUser user)
    {
        _context.Users.Update(user);

        return Task.CompletedTask;
    }


    //====================================================
    // UPDATE USER STATUS
    //====================================================

    public Task UpdateStatusAsync(
        ApplicationUser user,
        bool isActive)
    {
        user.IsActive = isActive;

        _context.Users.Update(user);

        return Task.CompletedTask;
    }


    //====================================================
    // SOFT DELETE USER
    //====================================================

    public Task SoftDeleteAsync(
        ApplicationUser user,
        string deletedBy)
    {
        user.IsDeleted = true;

        user.IsActive = false;

        user.DeletedOn =
            DateTime.UtcNow;

        user.DeletedBy =
            deletedBy;

        _context.Users.Update(user);

        return Task.CompletedTask;
    }


    //====================================================
    // SAVE CHANGES
    //====================================================

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}