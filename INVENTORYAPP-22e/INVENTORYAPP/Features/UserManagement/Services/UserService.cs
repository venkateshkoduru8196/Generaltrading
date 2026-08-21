using INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;
using INVENTORYAPP.Features.UserManagement.DTOs;
using INVENTORYAPP.Features.UserManagement.Interfaces;
using INVENTORYAPP.Models;

using Microsoft.AspNetCore.Identity;

namespace INVENTORYAPP.Features.UserManagement.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUserService _currentUser;

    public UserService(
        IUserRepository repository,
        UserManager<ApplicationUser> userManager,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _userManager = userManager;
        _currentUser = currentUser;
    }


    //====================================================
    // ADMIN
    //====================================================

    public async Task<PagedUserResponseDto> GetAdminsAsync(
        UserListRequestDto request)
    {
        EnsureAuthenticated();

        await EnsureSuperAdminAsync();

        ValidatePagination(request);

        var result =
            await _repository.GetUsersAsync(
                "Admin",
                null,
                request.Search,
                request.IsActive,
                request.PageNumber,
                request.PageSize);

        return await BuildPagedResponseAsync(
            result.Users,
            result.TotalRecords,
            request);
    }


    public async Task<UserResponseDto?> GetAdminByIdAsync(
        string userId)
    {
        EnsureAuthenticated();

        await EnsureSuperAdminAsync();

        var user =
            await _repository.GetByIdAsync(userId);

        if (user == null)
            return null;

        await EnsureUserHasRoleAsync(
            user,
            "Admin");

        return await MapToResponseAsync(user);
    }


    public async Task UpdateAdminAsync(
        UpdateUserRequestDto request)
    {
        EnsureAuthenticated();

        await EnsureSuperAdminAsync();

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            throw new InvalidOperationException(
                "User ID is required.");
        }

        var user =
            await _repository.GetByIdAsync(
                request.UserId);

        if (user == null)
        {
            throw new InvalidOperationException(
                "Admin not found.");
        }

        await EnsureUserHasRoleAsync(
            user,
            "Admin");

        await ValidateUserDataAsync(
            user,
            request.UserName,
            request.Email);

        //================================================
        // BASIC INFORMATION
        //================================================

        user.FullName =
            request.FullName.Trim();

        user.PhoneNumber =
            request.PhoneNumber?.Trim();

        //================================================
        // USERNAME
        //================================================

        if (!string.Equals(
                user.UserName,
                request.UserName,
                StringComparison.OrdinalIgnoreCase))
        {
            var result =
                await _userManager.SetUserNameAsync(
                    user,
                    request.UserName.Trim());

            EnsureIdentitySuccess(
                result,
                "Unable to update username.");
        }

        //================================================
        // EMAIL
        //================================================

        if (!string.Equals(
                user.Email,
                request.Email,
                StringComparison.OrdinalIgnoreCase))
        {
            var result =
                await _userManager.SetEmailAsync(
                    user,
                    request.Email.Trim());

            EnsureIdentitySuccess(
                result,
                "Unable to update email.");
        }

        //================================================
        // COMPANY
        //================================================

        user.CompanyId =
            request.CompanyId;

        //================================================
        // AUDIT
        //================================================

        user.ModifiedOn =
            DateTime.UtcNow;

        user.ModifiedBy =
            _currentUser.UserName;

        await _repository.UpdateAsync(user);

        await _repository.SaveChangesAsync();
    }


    public async Task UpdateAdminStatusAsync(
        UpdateUserStatusRequestDto request)
    {
        EnsureAuthenticated();

        await EnsureSuperAdminAsync();

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            throw new InvalidOperationException(
                "User ID is required.");
        }

        var user =
            await _repository.GetByIdAsync(
                request.UserId);

        if (user == null)
        {
            throw new InvalidOperationException(
                "Admin not found.");
        }

        await EnsureUserHasRoleAsync(
            user,
            "Admin");

        await _repository.UpdateStatusAsync(
            user,
            request.IsActive);

        user.ModifiedOn =
            DateTime.UtcNow;

        user.ModifiedBy =
            _currentUser.UserName;

        await _repository.SaveChangesAsync();
    }


    public async Task DeleteAdminAsync(
        string userId)
    {
        EnsureAuthenticated();

        await EnsureSuperAdminAsync();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidOperationException(
                "User ID is required.");
        }

        var user =
            await _repository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new InvalidOperationException(
                "Admin not found.");
        }

        await EnsureUserHasRoleAsync(
            user,
            "Admin");

        await _repository.SoftDeleteAsync(
            user,
            _currentUser.UserName);

        await _repository.SaveChangesAsync();
    }


    //====================================================
    // EMPLOYEE
    //====================================================

    public async Task<PagedUserResponseDto> GetEmployeesAsync(
        UserListRequestDto request)
    {
        EnsureAuthenticated();

        await EnsureSuperAdminOrAdminAsync();

        ValidatePagination(request);

        int? companyId;

        if (await IsSuperAdminAsync())
        {
            //================================================
            // SUPERADMIN
            //================================================

            companyId = null;
        }
        else
        {
            //================================================
            // ADMIN
            //================================================

            companyId =
                _currentUser.CompanyId;

            if (!companyId.HasValue)
            {
                throw new InvalidOperationException(
                    "Current user is not associated with a company.");
            }
        }

        var result =
            await _repository.GetUsersAsync(
                "Employee",
                companyId,
                request.Search,
                request.IsActive,
                request.PageNumber,
                request.PageSize);

        return await BuildPagedResponseAsync(
            result.Users,
            result.TotalRecords,
            request);
    }


    public async Task<UserResponseDto?> GetEmployeeByIdAsync(
        string userId)
    {
        EnsureAuthenticated();

        await EnsureSuperAdminOrAdminAsync();

        var user =
            await _repository.GetByIdAsync(userId);

        if (user == null)
            return null;

        await EnsureUserHasRoleAsync(
            user,
            "Employee");

        await EnsureCompanyAccessAsync(user);

        return await MapToResponseAsync(user);
    }


    public async Task UpdateEmployeeAsync(
        UpdateUserRequestDto request)
    {
        EnsureAuthenticated();

        await EnsureSuperAdminOrAdminAsync();

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            throw new InvalidOperationException(
                "User ID is required.");
        }

        var user =
            await _repository.GetByIdAsync(
                request.UserId);

        if (user == null)
        {
            throw new InvalidOperationException(
                "Employee not found.");
        }

        await EnsureUserHasRoleAsync(
            user,
            "Employee");

        //================================================
        // COMPANY SECURITY
        //================================================

        await EnsureCompanyAccessAsync(user);

        await ValidateUserDataAsync(
            user,
            request.UserName,
            request.Email);

        //================================================
        // BASIC INFORMATION
        //================================================

        user.FullName =
            request.FullName.Trim();

        user.PhoneNumber =
            request.PhoneNumber?.Trim();

        //================================================
        // USERNAME
        //================================================

        if (!string.Equals(
                user.UserName,
                request.UserName,
                StringComparison.OrdinalIgnoreCase))
        {
            var result =
                await _userManager.SetUserNameAsync(
                    user,
                    request.UserName.Trim());

            EnsureIdentitySuccess(
                result,
                "Unable to update username.");
        }

        //================================================
        // EMAIL
        //================================================

        if (!string.Equals(
                user.Email,
                request.Email,
                StringComparison.OrdinalIgnoreCase))
        {
            var result =
                await _userManager.SetEmailAsync(
                    user,
                    request.Email.Trim());

            EnsureIdentitySuccess(
                result,
                "Unable to update email.");
        }

        //================================================
        // COMPANY
        //================================================

        if (await IsSuperAdminAsync())
        {
            user.CompanyId =
                request.CompanyId;
        }
        else
        {
            //================================================
            // ADMIN CANNOT MOVE EMPLOYEE TO ANOTHER COMPANY
            //================================================

            user.CompanyId =
                _currentUser.CompanyId;
        }

        //================================================
        // AUDIT
        //================================================

        user.ModifiedOn =
            DateTime.UtcNow;

        user.ModifiedBy =
            _currentUser.UserName;

        await _repository.UpdateAsync(user);

        await _repository.SaveChangesAsync();
    }


    public async Task UpdateEmployeeStatusAsync(
        UpdateUserStatusRequestDto request)
    {
        EnsureAuthenticated();

        await EnsureSuperAdminOrAdminAsync();

        if (string.IsNullOrWhiteSpace(request.UserId))
        {
            throw new InvalidOperationException(
                "User ID is required.");
        }

        var user =
            await _repository.GetByIdAsync(
                request.UserId);

        if (user == null)
        {
            throw new InvalidOperationException(
                "Employee not found.");
        }

        await EnsureUserHasRoleAsync(
            user,
            "Employee");

        await EnsureCompanyAccessAsync(user);

        await _repository.UpdateStatusAsync(
            user,
            request.IsActive);

        user.ModifiedOn =
            DateTime.UtcNow;

        user.ModifiedBy =
            _currentUser.UserName;

        await _repository.SaveChangesAsync();
    }


    public async Task DeleteEmployeeAsync(
        string userId)
    {
        EnsureAuthenticated();

        await EnsureSuperAdminOrAdminAsync();

        if (string.IsNullOrWhiteSpace(userId))
        {
            throw new InvalidOperationException(
                "User ID is required.");
        }

        var user =
            await _repository.GetByIdAsync(userId);

        if (user == null)
        {
            throw new InvalidOperationException(
                "Employee not found.");
        }

        await EnsureUserHasRoleAsync(
            user,
            "Employee");

        await EnsureCompanyAccessAsync(user);

        await _repository.SoftDeleteAsync(
            user,
            _currentUser.UserName);

        await _repository.SaveChangesAsync();
    }


    //====================================================
    // AUTHORIZATION HELPERS
    //====================================================

    private void EnsureAuthenticated()
    {
        if (!_currentUser.IsAuthenticated)
        {
            throw new UnauthorizedAccessException(
                "User is not authenticated.");
        }
    }


    private async Task EnsureSuperAdminAsync()
    {
        if (!await IsSuperAdminAsync())
        {
            throw new UnauthorizedAccessException(
                "Only SuperAdmin can perform this operation.");
        }
    }


    private async Task EnsureSuperAdminOrAdminAsync()
    {
        var isSuperAdmin =
            await IsSuperAdminAsync();

        if (isSuperAdmin)
            return;

        var currentUser =
            await _userManager.FindByIdAsync(
                _currentUser.UserId);

        if (currentUser == null)
        {
            throw new UnauthorizedAccessException(
                "Current user was not found.");
        }

        var isAdmin =
            await _userManager.IsInRoleAsync(
                currentUser,
                "Admin");

        if (!isAdmin)
        {
            throw new UnauthorizedAccessException(
                "Only SuperAdmin or Admin can perform this operation.");
        }
    }


    private async Task<bool> IsSuperAdminAsync()
    {
        var currentUser =
            await _userManager.FindByIdAsync(
                _currentUser.UserId);

        if (currentUser == null)
            return false;

        return await _userManager.IsInRoleAsync(
            currentUser,
            "SuperAdmin");
    }


    //====================================================
    // COMPANY SECURITY
    //====================================================

    private async Task EnsureCompanyAccessAsync(
        ApplicationUser targetUser)
    {
        //================================================
        // SUPERADMIN CAN ACCESS ALL COMPANIES
        //================================================

        if (await IsSuperAdminAsync())
            return;

        var currentCompanyId =
            _currentUser.CompanyId;

        if (!currentCompanyId.HasValue)
        {
            throw new UnauthorizedAccessException(
                "Current user is not associated with a company.");
        }

        if (targetUser.CompanyId !=
            currentCompanyId.Value)
        {
            throw new UnauthorizedAccessException(
                "You do not have access to this user's company.");
        }
    }


    //====================================================
    // ROLE VALIDATION
    //====================================================

    private async Task EnsureUserHasRoleAsync(
        ApplicationUser user,
        string expectedRole)
    {
        var role =
            await _repository.GetRoleAsync(user);

        if (!string.Equals(
                role.RoleName,
                expectedRole,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException(
                $"User is not an {expectedRole}.");
        }
    }


    //====================================================
    // USER VALIDATION
    //====================================================

    private async Task ValidateUserDataAsync(
        ApplicationUser currentUser,
        string userName,
        string email)
    {
        if (string.IsNullOrWhiteSpace(userName))
        {
            throw new InvalidOperationException(
                "Username is required.");
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            throw new InvalidOperationException(
                "Email is required.");
        }

        var existingUser =
            await _userManager.FindByNameAsync(
                userName.Trim());

        if (existingUser != null &&
            existingUser.Id != currentUser.Id)
        {
            throw new InvalidOperationException(
                "Username already exists.");
        }

        var existingEmail =
            await _userManager.FindByEmailAsync(
                email.Trim());

        if (existingEmail != null &&
            existingEmail.Id != currentUser.Id)
        {
            throw new InvalidOperationException(
                "Email already exists.");
        }
    }


    //====================================================
    // PAGINATION VALIDATION
    //====================================================

    private static void ValidatePagination(
        UserListRequestDto request)
    {
        if (request.PageNumber < 1)
        {
            request.PageNumber = 1;
        }

        if (request.PageSize < 1)
        {
            request.PageSize = 20;
        }

        if (request.PageSize > 100)
        {
            request.PageSize = 100;
        }
    }


    //====================================================
    // RESPONSE MAPPING
    //====================================================

    private async Task<UserResponseDto>
        MapToResponseAsync(
            ApplicationUser user)
    {
        var role =
            await _repository.GetRoleAsync(user);

        return new UserResponseDto
        {
            //================================================
            // USER
            //================================================

            UserId =
                user.Id,

            UserName =
                user.UserName ?? string.Empty,

            FullName =
                user.FullName,

            Email =
                user.Email ?? string.Empty,

            PhoneNumber =
                user.PhoneNumber ?? string.Empty,


            //================================================
            // ROLE
            //================================================

            RoleId =
                role.RoleId ?? string.Empty,

            RoleName =
                role.RoleName ?? string.Empty,


            //================================================
            // COMPANY
            //================================================

            CompanyId =
                user.CompanyId,

            CompanyCode =
                user.Company?.CompanyCode
                ?? string.Empty,

            CompanyName =
                user.Company?.CompanyName
                ?? string.Empty,


            //================================================
            // STATUS
            //================================================

            IsActive =
                user.IsActive,

            IsDeleted =
                user.IsDeleted,


            //================================================
            // AUDIT
            //================================================

            CreatedOn =
                user.CreatedOn,

            CreatedBy =
                user.CreatedBy,

            ModifiedOn =
                user.ModifiedOn,

            ModifiedBy =
                user.ModifiedBy,

            DeletedOn =
                user.DeletedOn,

            DeletedBy =
                user.DeletedBy,

            LastLoginOn =
                user.LastLoginOn
        };
    }


    //====================================================
    // PAGED RESPONSE
    //====================================================

    private async Task<PagedUserResponseDto>
        BuildPagedResponseAsync(
            List<ApplicationUser> users,
            int totalRecords,
            UserListRequestDto request)
    {
        var items =
            new List<UserResponseDto>();

        foreach (var user in users)
        {
            items.Add(
                await MapToResponseAsync(user));
        }

        var totalPages =
            totalRecords == 0
                ? 0
                : (int)Math.Ceiling(
                    totalRecords /
                    (double)request.PageSize);

        return new PagedUserResponseDto
        {
            Items =
                items,

            PageNumber =
                request.PageNumber,

            PageSize =
                request.PageSize,

            TotalRecords =
                totalRecords,

            TotalPages =
                totalPages,

            HasPreviousPage =
                request.PageNumber > 1,

            HasNextPage =
                request.PageNumber < totalPages
        };
    }


    //====================================================
    // IDENTITY RESULT VALIDATION
    //====================================================

    private static void EnsureIdentitySuccess(
        IdentityResult result,
        string defaultMessage)
    {
        if (result.Succeeded)
            return;

        var errors =
            string.Join(
                "; ",
                result.Errors.Select(x =>
                    x.Description));

        throw new InvalidOperationException(
            string.IsNullOrWhiteSpace(errors)
                ? defaultMessage
                : errors);
    }
}