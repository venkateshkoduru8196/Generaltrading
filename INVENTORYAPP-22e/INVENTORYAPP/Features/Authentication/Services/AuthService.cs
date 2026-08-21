using INVENTORYAPP.Data;
using INVENTORYAPP.Infrastructure.Exceptions;
using INVENTORYAPP.Infrastructure.Jwt;
using INVENTORYAPP.Models;

using INVENTORYAPP.Features.Authentication.DTOs.Auth;
using INVENTORYAPP.Features.Authentication.Interfaces;
using INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Authentication.Services;

public class AuthService : IAuthService
{
    //====================================================
    // DEPENDENCIES
    //====================================================

    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    private readonly JwtTokenGenerator _jwtTokenGenerator;

    private readonly AppDbContext _context;

    private readonly ICurrentUserService _currentUser;


    //====================================================
    // CONSTRUCTOR
    //====================================================

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        JwtTokenGenerator jwtTokenGenerator,
        AppDbContext context,
        ICurrentUserService currentUser)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _context = context;
        _currentUser = currentUser;
    }


    //====================================================
    // GENERATE REFRESH TOKEN
    //====================================================

    private static string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString("N")
             + Guid.NewGuid().ToString("N");
    }


    //====================================================
    // CUSTOMER REGISTRATION
    //====================================================

    public async Task<AuthResponseDto> RegisterCustomerAsync(
        RegisterRequestDto dto)
    {
        //================================================
        // VALIDATION
        //================================================

        if (dto == null)
        {
            throw new AppException(
                "Registration information is required.");
        }

        if (string.IsNullOrWhiteSpace(dto.Email))
        {
            throw new AppException(
                "Email is required.");
        }

        if (string.IsNullOrWhiteSpace(dto.UserName))
        {
            throw new AppException(
                "Username is required.");
        }


        //================================================
        // EMAIL CHECK
        //================================================

        var existingUser =
            await _userManager.FindByEmailAsync(
                dto.Email.Trim());

        if (existingUser != null)
        {
            throw new AppException(
                "Email already exists.");
        }


        //================================================
        // USERNAME CHECK
        //================================================

        var existingUsername =
            await _userManager.FindByNameAsync(
                dto.UserName.Trim());

        if (existingUsername != null)
        {
            throw new AppException(
                "Username already exists.");
        }


        //================================================
        // CREATE USER
        //================================================

        var user = new ApplicationUser
        {
            FullName =
                $"{dto.FirstName} {dto.LastName}".Trim(),

            UserName =
                dto.UserName.Trim(),

            Email =
                dto.Email.Trim(),

            PhoneNumber =
                dto.PhoneNumber?.Trim() ?? string.Empty,

            IsActive =
                true,

            IsDeleted =
                false,

            CreatedOn =
                DateTime.UtcNow,

            CreatedBy =
                "SYSTEM"
        };


        //================================================
        // CREATE IDENTITY USER
        //================================================

        var result =
            await _userManager.CreateAsync(
                user,
                dto.Password);

        if (!result.Succeeded)
        {
            throw new AppException(
                string.Join(
                    ", ",
                    result.Errors.Select(
                        x => x.Description)));
        }


        //================================================
        // ASSIGN CUSTOMER ROLE
        //================================================

        var roleResult =
            await _userManager.AddToRoleAsync(
                user,
                "Customer");

        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);

            throw new AppException(
                string.Join(
                    ", ",
                    roleResult.Errors.Select(
                        x => x.Description)));
        }


        //================================================
        // GET ROLES
        //================================================

        var roles =
            await _userManager.GetRolesAsync(
                user);


        //================================================
        // ACCESS TOKEN
        //================================================

        var accessToken =
            _jwtTokenGenerator.GenerateToken(
                user,
                roles);


        //================================================
        // REFRESH TOKEN
        //================================================

        var refreshToken =
            GenerateRefreshToken();


        _context.RefreshTokens.Add(
            new RefreshToken
            {
                Token =
                    refreshToken,

                UserId =
                    user.Id,

                CreatedOn =
                    DateTime.UtcNow,

                ExpiresOn =
                    DateTime.UtcNow.AddDays(7),

                IsRevoked =
                    false
            });


        await _context.SaveChangesAsync();


        //================================================
        // ROLE DETAILS
        //================================================

        var roleName =
            roles.FirstOrDefault();

        var role =
            await _context.Roles
                .FirstOrDefaultAsync(
                    x => x.Name == roleName);


        //================================================
        // RESPONSE
        //================================================

        return new AuthResponseDto
        {
            UserId =
                user.Id,

            UserName =
                user.UserName ?? string.Empty,

            FullName =
                user.FullName,

            Email =
                user.Email ?? string.Empty,

            RoleId =
                role?.Id ?? string.Empty,

            RoleName =
                role?.Name ?? string.Empty,

            AccessToken =
                accessToken,

            RefreshToken =
                refreshToken
        };
    }


    //====================================================
    // LOGIN
    //====================================================

    public async Task<AuthResponseDto> LoginAsync(
        LoginRequestDto dto)
    {
        //================================================
        // FIND USER
        //================================================

        var user =
            await _userManager.FindByNameAsync(
                dto.UserName);

        if (user == null)
        {
            throw new AppException(
                "Invalid username or password.");
        }


        //================================================
        // DELETED CHECK
        //================================================

        if (user.IsDeleted)
        {
            throw new AppException(
                "Your account is no longer available.");
        }


        //================================================
        // ACTIVE CHECK
        //================================================

        if (!user.IsActive)
        {
            throw new AppException(
                "Your account is inactive.");
        }


        //================================================
        // PASSWORD CHECK
        //================================================

        var result =
            await _signInManager
                .CheckPasswordSignInAsync(
                    user,
                    dto.Password,
                    false);

        if (!result.Succeeded)
        {
            throw new AppException(
                "Invalid username or password.");
        }


        //================================================
        // GET ROLES
        //================================================

        var roles =
            await _userManager.GetRolesAsync(
                user);

        var roleName =
            roles.FirstOrDefault();


        //================================================
        // ROLE DETAILS
        //================================================

        var role =
            await _context.Roles
                .FirstOrDefaultAsync(
                    x => x.Name == roleName);


        //================================================
        // ACCESS TOKEN
        //================================================

        var accessToken =
            _jwtTokenGenerator.GenerateToken(
                user,
                roles);


        //================================================
        // REFRESH TOKEN
        //================================================

        var refreshToken =
            GenerateRefreshToken();


        _context.RefreshTokens.Add(
            new RefreshToken
            {
                Token =
                    refreshToken,

                UserId =
                    user.Id,

                CreatedOn =
                    DateTime.UtcNow,

                ExpiresOn =
                    DateTime.UtcNow.AddDays(7),

                IsRevoked =
                    false
            });


        //================================================
        // LAST LOGIN
        //================================================

        user.LastLoginOn =
            DateTime.UtcNow;


        var updateResult =
            await _userManager.UpdateAsync(
                user);

        if (!updateResult.Succeeded)
        {
            throw new AppException(
                string.Join(
                    ", ",
                    updateResult.Errors.Select(
                        x => x.Description)));
        }


        await _context.SaveChangesAsync();


        //================================================
        // RESPONSE
        //================================================

        return new AuthResponseDto
        {
            UserId =
                user.Id,

            UserName =
                user.UserName ?? string.Empty,

            FullName =
                user.FullName,

            Email =
                user.Email ?? string.Empty,

            RoleId =
                role?.Id ?? string.Empty,

            RoleName =
                role?.Name ?? string.Empty,

            AccessToken =
                accessToken,

            RefreshToken =
                refreshToken,

            ExpiresAt =
                DateTime.UtcNow.AddMinutes(30)
        };
    }


    //====================================================
    // REFRESH TOKEN
    //====================================================

    public async Task<AuthResponseDto> RefreshTokenAsync(
        string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            throw new AppException(
                "Refresh token is required.");
        }


        //================================================
        // FIND TOKEN
        //================================================

        var existingToken =
            await _context.RefreshTokens
                .FirstOrDefaultAsync(
                    x =>
                        x.Token == refreshToken &&
                        !x.IsRevoked);


        if (existingToken == null)
        {
            throw new AppException(
                "Invalid refresh token.");
        }


        //================================================
        // EXPIRATION
        //================================================

        if (existingToken.ExpiresOn <
            DateTime.UtcNow)
        {
            throw new AppException(
                "Refresh token has expired. Please login again.");
        }


        //================================================
        // FIND USER
        //================================================

        var user =
            await _userManager.FindByIdAsync(
                existingToken.UserId);


        if (user == null)
        {
            throw new AppException(
                "User not found.");
        }


        //================================================
        // USER STATUS
        //================================================

        if (user.IsDeleted)
        {
            throw new AppException(
                "Your account is no longer available.");
        }


        if (!user.IsActive)
        {
            throw new AppException(
                "Your account is inactive.");
        }


        //================================================
        // GET ROLES
        //================================================

        var roles =
            await _userManager.GetRolesAsync(
                user);


        //================================================
        // ACCESS TOKEN
        //================================================

        var newAccessToken =
            _jwtTokenGenerator.GenerateToken(
                user,
                roles);


        //================================================
        // NEW REFRESH TOKEN
        //================================================

        var newRefreshToken =
            GenerateRefreshToken();


        existingToken.IsRevoked =
            true;


        _context.RefreshTokens.Add(
            new RefreshToken
            {
                Token =
                    newRefreshToken,

                UserId =
                    user.Id,

                CreatedOn =
                    DateTime.UtcNow,

                ExpiresOn =
                    DateTime.UtcNow.AddDays(7),

                IsRevoked =
                    false
            });


        await _context.SaveChangesAsync();


        //================================================
        // ROLE
        //================================================

        var roleName =
            roles.FirstOrDefault();

        var role =
            await _context.Roles
                .FirstOrDefaultAsync(
                    x => x.Name == roleName);


        //================================================
        // RESPONSE
        //================================================

        return new AuthResponseDto
        {
            UserId =
                user.Id,

            UserName =
                user.UserName ?? string.Empty,

            FullName =
                user.FullName,

            Email =
                user.Email ?? string.Empty,

            RoleId =
                role?.Id ?? string.Empty,

            RoleName =
                role?.Name ?? string.Empty,

            AccessToken =
                newAccessToken,

            RefreshToken =
                newRefreshToken,

            ExpiresAt =
                DateTime.UtcNow.AddMinutes(30)
        };
    }


    //====================================================
    // CREATE ADMIN / EMPLOYEE
    //====================================================

    public async Task<bool> CreateUserAsync(
        CreateUserDto dto)
    {
        //================================================
        // REQUEST VALIDATION
        //================================================

        if (dto == null)
        {
            throw new AppException(
                "User information is required.");
        }


        if (string.IsNullOrWhiteSpace(
            dto.FullName))
        {
            throw new AppException(
                "Full name is required.");
        }


        if (string.IsNullOrWhiteSpace(
            dto.UserName))
        {
            throw new AppException(
                "Username is required.");
        }


        if (string.IsNullOrWhiteSpace(
            dto.Email))
        {
            throw new AppException(
                "Email is required.");
        }


        if (string.IsNullOrWhiteSpace(
            dto.Password))
        {
            throw new AppException(
                "Password is required.");
        }


        if (string.IsNullOrWhiteSpace(
            dto.ConfirmPassword))
        {
            throw new AppException(
                "Confirm Password is required.");
        }


        if (string.IsNullOrWhiteSpace(
            dto.EditPassword))
        {
            throw new AppException(
                "Edit Password is required.");
        }


        if (string.IsNullOrWhiteSpace(
            dto.ConfirmEditPassword))
        {
            throw new AppException(
                "Confirm Edit Password is required.");
        }


        //================================================
        // PASSWORD CONFIRMATION
        //================================================

        if (dto.Password !=
            dto.ConfirmPassword)
        {
            throw new AppException(
                "Password and Confirm Password do not match.");
        }


        if (dto.EditPassword !=
            dto.ConfirmEditPassword)
        {
            throw new AppException(
                "Edit Password and Confirm Edit Password do not match.");
        }


        //================================================
        // ROLE VALIDATION
        //================================================

        if (string.IsNullOrWhiteSpace(
            dto.Role))
        {
            throw new AppException(
                "Role is required.");
        }


        var role =
            await _context.Roles
                .FirstOrDefaultAsync(
                    x => x.Name == dto.Role);


        if (role == null)
        {
            throw new AppException(
                $"Role '{dto.Role}' does not exist.");
        }


        //================================================
        // EMAIL DUPLICATE CHECK
        //================================================

        var existingEmailUser =
            await _userManager.FindByEmailAsync(
                dto.Email.Trim());


        if (existingEmailUser != null)
        {
            throw new AppException(
                "Email already exists.");
        }


        //================================================
        // USERNAME DUPLICATE CHECK
        //================================================

        var existingUsernameUser =
            await _userManager.FindByNameAsync(
                dto.UserName.Trim());


        if (existingUsernameUser != null)
        {
            throw new AppException(
                "Username already exists.");
        }


        //================================================
        // DETERMINE COMPANY
        //================================================

        int? companyId;


        if (dto.Role == "Admin")
        {
            //================================================
            // ADMIN
            //
            // SuperAdmin selects company from frontend.
            //================================================

            if (!dto.CompanyId.HasValue)
            {
                throw new AppException(
                    "Company is required for an administrator.");
            }


            companyId =
                dto.CompanyId.Value;
        }
        else
        {
            //================================================
            // EMPLOYEE
            //
            // Employee automatically belongs to
            // logged-in user's company.
            //================================================

            companyId =
                _currentUser.CompanyId;


            if (!companyId.HasValue)
            {
                throw new AppException(
                    "Your account is not associated with a company.");
            }
        }


        //================================================
        // VERIFY COMPANY
        //
        // Company model has:
        // CompanyId
        // IsActive
        //
        // It does NOT have IsDeleted.
        //================================================

        var companyExists =
            await _context.Companies
                .AnyAsync(
                    x =>
                        x.CompanyId ==
                            companyId.Value &&

                        x.IsActive);


        if (!companyExists)
        {
            throw new AppException(
                "Selected company does not exist or is inactive.");
        }


        //================================================
        // CREATED BY
        //================================================

        var createdBy =
            _currentUser.UserName;


        if (string.IsNullOrWhiteSpace(
            createdBy))
        {
            createdBy =
                "SYSTEM";
        }


        //================================================
        // CREATE APPLICATION USER
        //================================================

        var user =
            new ApplicationUser
            {
                FullName =
                    dto.FullName.Trim(),

                UserName =
                    dto.UserName.Trim(),

                Email =
                    dto.Email.Trim(),

                PhoneNumber =
                    dto.PhoneNumber?.Trim()
                    ?? string.Empty,

                CompanyId =
                    companyId,

                IsActive =
                    true,

                IsDeleted =
                    false,

                CreatedOn =
                    DateTime.UtcNow,

                CreatedBy =
                    createdBy
            };


        //================================================
        // CREATE IDENTITY USER
        //================================================

        var createResult =
            await _userManager.CreateAsync(
                user,
                dto.Password);


        if (!createResult.Succeeded)
        {
            throw new AppException(
                string.Join(
                    ", ",
                    createResult.Errors.Select(
                        x => x.Description)));
        }


        //================================================
        // EVERYTHING AFTER IDENTITY CREATION
        //================================================

        try
        {
            //============================================
            // HASH EDIT PASSWORD
            //============================================

            var passwordHasher =
                new PasswordHasher<ApplicationUser>();


            user.EditPasswordHash =
                passwordHasher.HashPassword(
                    user,
                    dto.EditPassword);


            //============================================
            // SAVE EDIT PASSWORD HASH
            //============================================

            var updateResult =
                await _userManager.UpdateAsync(
                    user);


            if (!updateResult.Succeeded)
            {
                throw new AppException(
                    string.Join(
                        ", ",
                        updateResult.Errors.Select(
                            x => x.Description)));
            }


            //============================================
            // ASSIGN ROLE
            //============================================

            var roleResult =
                await _userManager.AddToRoleAsync(
                    user,
                    dto.Role);


            if (!roleResult.Succeeded)
            {
                throw new AppException(
                    string.Join(
                        ", ",
                        roleResult.Errors.Select(
                            x => x.Description)));
            }


            //============================================
            // SUCCESS
            //============================================

            return true;
        }
        catch
        {
            //============================================
            // CLEANUP
            //
            // If edit-password or role assignment fails,
            // remove the newly-created Identity user.
            //============================================

            var createdUser =
                await _userManager.FindByIdAsync(
                    user.Id);


            if (createdUser != null)
            {
                await _userManager.DeleteAsync(
                    createdUser);
            }


            throw;
        }
    }


    //====================================================
    // VERIFY EDIT PASSWORD
    //====================================================

    public async Task VerifyEditPasswordAsync(
        string userId,
        string password)
    {
        //================================================
        // FIND USER
        //================================================

        var user =
            await _userManager.FindByIdAsync(
                userId);


        if (user == null)
        {
            throw new AppException(
                "User not found.");
        }


        //================================================
        // DELETED CHECK
        //================================================

        if (user.IsDeleted)
        {
            throw new AppException(
                "User is deleted.");
        }


        //================================================
        // EDIT PASSWORD CHECK
        //================================================

        if (string.IsNullOrWhiteSpace(
            user.EditPasswordHash))
        {
            throw new AppException(
                "Edit Password is not configured.");
        }


        //================================================
        // VERIFY HASH
        //================================================

        var passwordHasher =
            new PasswordHasher<ApplicationUser>();


        var result =
            passwordHasher.VerifyHashedPassword(
                user,
                user.EditPasswordHash,
                password);


        if (result ==
            PasswordVerificationResult.Failed)
        {
            throw new AppException(
                "Invalid Edit Password.");
        }
    }


    //====================================================
    // SET SUPER ADMIN EDIT PASSWORD
    //====================================================

    public async Task SetSuperAdminEditPasswordAsync()
    {
        //================================================
        // FIND SUPER ADMIN
        //================================================

        var user =
            await _userManager.FindByNameAsync(
                "superadmin");


        if (user == null)
        {
            throw new AppException(
                "SuperAdmin not found.");
        }


        //================================================
        // HASH EDIT PASSWORD
        //================================================

        var passwordHasher =
            new PasswordHasher<ApplicationUser>();


        user.EditPasswordHash =
            passwordHasher.HashPassword(
                user,
                "1234");


        //================================================
        // SAVE
        //================================================

        var result =
            await _userManager.UpdateAsync(
                user);


        if (!result.Succeeded)
        {
            throw new AppException(
                string.Join(
                    ", ",
                    result.Errors.Select(
                        x => x.Description)));
        }
    }
}












//using INVENTORYAPP.Data;
//using INVENTORYAPP.Infrastructure.Jwt;
//using INVENTORYAPP.Models;

//using INVENTORYAPP.Infrastructure.Exceptions;

//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using INVENTORYAPP.Features.Authentication.Interfaces;
//using INVENTORYAPP.Features.Authentication.DTOs.Auth;
//using INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;

//namespace INVENTORYAPP.Features.Authentication.Services;


//public class AuthService : IAuthService
//{
//    private readonly UserManager<ApplicationUser> _userManager;

//    private readonly SignInManager<ApplicationUser> _signInManager;

//    private readonly JwtTokenGenerator _jwtTokenGenerator;

//    private readonly AppDbContext _context;

//    private readonly ICurrentUserService _currentUser;

//    public AuthService(
//        UserManager<ApplicationUser> userManager,
//        SignInManager<ApplicationUser> signInManager,
//        JwtTokenGenerator jwtTokenGenerator,
//        AppDbContext context,
//         ICurrentUserService currentUser)
//    {
//        _userManager = userManager;
//        _signInManager = signInManager;
//        _jwtTokenGenerator = jwtTokenGenerator;
//        _context = context;
//        _currentUser = currentUser;

//    }

//    private static string GenerateRefreshToken()
//    {
//        return Guid.NewGuid().ToString()
//             + Guid.NewGuid().ToString();
//    }




//    public async Task<AuthResponseDto> RegisterCustomerAsync(
//    RegisterRequestDto dto)
//    {
//        var existingUser =
//            await _userManager.FindByEmailAsync(
//                dto.Email);

//        if (existingUser != null)
//        {
//            throw new AppException(
//     "Email already exists");
//        }

//        var user = new ApplicationUser
//        {
//            FullName =
//                $"{dto.FirstName} {dto.LastName}",

//            UserName = dto.UserName,

//            Email = dto.Email,

//            PhoneNumber = dto.PhoneNumber,

//            IsActive = true,

//            CreatedOn = DateTime.UtcNow
//        };

//        var result =
//            await _userManager.CreateAsync(
//                user,
//                dto.Password);

//        if (!result.Succeeded)
//        {
//            throw new AppException(
//     string.Join(", ",
//         result.Errors.Select(x => x.Description)));
//        }

//        await _userManager.AddToRoleAsync(
//            user,
//            "Customer");

//        var roles =
//            await _userManager.GetRolesAsync(
//                user);

//        var accessToken =
//            _jwtTokenGenerator.GenerateToken(
//                user,
//                roles);

//        var refreshToken =
//            GenerateRefreshToken();

//        var refreshTokenEntity =
//            new RefreshToken
//            {
//                Token = refreshToken,
//                UserId = user.Id,
//                CreatedOn = DateTime.UtcNow,
//                ExpiresOn = DateTime.UtcNow.AddDays(7)
//            };

//        _context.RefreshTokens.Add(
//            refreshTokenEntity);

//        await _context.SaveChangesAsync();



//        var roleName = roles.FirstOrDefault();

//        var role = await _context.Roles
//            .FirstOrDefaultAsync(x =>
//                x.Name == roleName);

//        return new AuthResponseDto
//        {
//            UserId = user.Id,

//            UserName = user.UserName!,

//            FullName = user.FullName,

//            Email = user.Email!,

//            RoleId = role?.Id ?? "",

//            RoleName = role?.Name ?? "",

//            AccessToken = accessToken,

//            RefreshToken = refreshToken
//        };





//    }



//    public async Task<AuthResponseDto> LoginAsync(
//  LoginRequestDto dto)
//    {
//        var user =
//            await _userManager.FindByNameAsync(
//                dto.UserName);


//        if (user == null)
//        {
//            throw new AppException(
//                "Invalid username or password.");
//        }






//        if (!user.IsActive)
//        {
//            throw new AppException(
//                "Your account is inactive.");
//        }

//        var result =
//            await _signInManager.CheckPasswordSignInAsync(
//                user,
//                dto.Password,
//                false);

//        if (!result.Succeeded)
//        {
//            throw new AppException(
//                "Invalid username or password.");
//        }

//        var roles =
//            await _userManager.GetRolesAsync(
//                user);


//        var roleName =
//    roles.FirstOrDefault();

//        var role =
//            await _context.Roles
//                .FirstOrDefaultAsync(x =>
//                    x.Name == roleName);


//        var accessToken =
//            _jwtTokenGenerator.GenerateToken(
//                user,
//                roles);

//        var refreshToken =
//            GenerateRefreshToken();

//        _context.RefreshTokens.Add(
//            new RefreshToken
//            {
//                Token = refreshToken,
//                UserId = user.Id,
//                CreatedOn = DateTime.UtcNow,
//                ExpiresOn = DateTime.UtcNow.AddDays(7),
//                IsRevoked = false
//            });

//        user.LastLoginOn =
//            DateTime.UtcNow;

//        await _context.SaveChangesAsync();

//        await _userManager.UpdateAsync(
//            user);




//        return new AuthResponseDto
//        {
//            UserId = user.Id,

//            UserName = user.UserName!,

//            FullName = user.FullName,

//            Email = user.Email!,

//            RoleId = role?.Id ?? "",

//            RoleName = role?.Name ?? "",

//            AccessToken = accessToken,

//            RefreshToken = refreshToken,

//            ExpiresAt =
//        DateTime.UtcNow.AddMinutes(30)
//        };






//    }




//    public async Task<AuthResponseDto> RefreshTokenAsync(
//    string refreshToken)
//    {
//        var existingToken =
//            await _context.RefreshTokens
//                .FirstOrDefaultAsync(x =>
//                    x.Token == refreshToken &&
//                    !x.IsRevoked);

//        if (existingToken == null)
//        {
//            throw new AppException(
//                "Invalid refresh token.");
//        }


//        if (existingToken.ExpiresOn < DateTime.UtcNow)
//        {
//            throw new AppException(
//                "Refresh token has expired. Please login again.");
//        }


//        var user =
//            await _userManager.FindByIdAsync(
//                existingToken.UserId);



//        if (user == null)
//        {
//            throw new AppException(
//                "User not found.");
//        }



//        var roles =
//            await _userManager.GetRolesAsync(
//                user);

//        var newAccessToken =
//            _jwtTokenGenerator.GenerateToken(
//                user,
//                roles);

//        var newRefreshToken =
//            GenerateRefreshToken();

//        existingToken.IsRevoked = true;

//        _context.RefreshTokens.Add(
//            new RefreshToken
//            {
//                Token = newRefreshToken,
//                UserId = user.Id,
//                CreatedOn = DateTime.UtcNow,
//                ExpiresOn = DateTime.UtcNow.AddDays(7),
//                IsRevoked = false
//            });

//        await _context.SaveChangesAsync();




//        var roleName = roles.FirstOrDefault();

//        var role = await _context.Roles
//            .FirstOrDefaultAsync(x =>
//                x.Name == roleName);

//        return new AuthResponseDto
//        {
//            UserId = user.Id,

//            UserName = user.UserName!,

//            FullName = user.FullName,

//            Email = user.Email!,

//            RoleId = role?.Id ?? "",

//            RoleName = role?.Name ?? "",

//            AccessToken = newAccessToken,

//            RefreshToken = newRefreshToken,

//            ExpiresAt =
//                DateTime.UtcNow.AddMinutes(30)
//        };







//    }


//    public async Task<bool> CreateUserAsync(
//    CreateUserDto dto)
//    {


//        if (dto.Password != dto.ConfirmPassword)
//        {
//            throw new AppException("Password and Confirm Password do not match.");
//        }

//        if (dto.EditPassword != dto.ConfirmEditPassword)
//        {
//            throw new AppException("Edit Password and Confirm Edit Password do not match.");
//        }



//          var existingUser =
//            await _userManager.FindByEmailAsync(
//                dto.Email);

//        if (existingUser != null)
//        {

//            throw new AppException(
//    "Email already exists");


//        }


//        // ==========================================
//        // Determine Company
//        // ==========================================

//        int? companyId;

//        if (dto.Role == "Admin")
//        {
//            // SuperAdmin selects the company
//            companyId = dto.CompanyId;
//        }
//        else
//        {
//            // Employee automatically belongs to logged-in Admin's company
//            companyId = _currentUser.CompanyId;
//        }

//        // ==========================================
//        // Create User
//        // ==========================================

//        var user = new ApplicationUser
//        {
//            FullName = dto.FullName,
//            UserName = dto.UserName,
//            Email = dto.Email,
//            PhoneNumber = dto.PhoneNumber,

//            CompanyId = companyId,

//            IsActive = true,
//            IsDeleted = false,

//            CreatedOn = DateTime.UtcNow,
//            CreatedBy = _currentUser.UserName
//        };

//        //var user =
//        //    new ApplicationUser
//        //    {
//        //        FullName = dto.FullName,
//        //        UserName = dto.UserName,
//        //        Email = dto.Email,
//        //        PhoneNumber = dto.PhoneNumber,
//        //        IsActive = true,
//        //        CreatedOn = DateTime.UtcNow
//        //    };

//        var result =
//            await _userManager.CreateAsync(
//                user,
//                dto.Password);

//        if (!result.Succeeded)
//        {
//            throw new AppException(
//  string.Join(", ",
//  result.Errors.Select(x => x.Description)));
//        }


//        var passwordHasher = new PasswordHasher<ApplicationUser>();

//        user.EditPasswordHash =
//            passwordHasher.HashPassword(
//                user,
//                dto.EditPassword);

//        await _userManager.UpdateAsync(user);





//        await _userManager.AddToRoleAsync(
//            user,
//            dto.Role);

//        return true;
//    }

//    public async Task VerifyEditPasswordAsync(
//    string userId,
//    string password)
//    {
//        var user = await _userManager.FindByIdAsync(userId);

//        if (user == null)
//            throw new AppException("User not found.");

//        if (string.IsNullOrWhiteSpace(user.EditPasswordHash))
//            throw new AppException("Edit Password is not configured.");

//        var passwordHasher = new PasswordHasher<ApplicationUser>();

//        var result = passwordHasher.VerifyHashedPassword(
//            user,
//            user.EditPasswordHash,
//            password);

//        if (result == PasswordVerificationResult.Failed)
//        {
//            throw new AppException("Invalid Edit Password.");
//        }

//        // Password verified successfully.
//    }



//    public async Task SetSuperAdminEditPasswordAsync()
//    {
//        var user = await _userManager.FindByNameAsync("superadmin");

//        if (user == null)
//        {
//            throw new AppException("SuperAdmin not found.");
//        }

//        var passwordHasher = new PasswordHasher<ApplicationUser>();

//        user.EditPasswordHash =
//            passwordHasher.HashPassword(user, "1234");

//        await _userManager.UpdateAsync(user);
//    }
















//}