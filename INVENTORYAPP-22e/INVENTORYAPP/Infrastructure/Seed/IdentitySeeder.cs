//using INVENTORYAPP.Models;
//using Microsoft.AspNetCore.Identity;

//namespace INVENTORYAPP.Infrastructure.Seed;

//public static class IdentitySeeder
//{
//    public static async Task SeedAsync(
//        UserManager<ApplicationUser> userManager,
//        RoleManager<ApplicationRole> roleManager)
//    {
//        string[] roles =
//        {
//            "SuperAdmin",
//            "Admin",
//            "Employee",
//            "Customer"
//        };

//        foreach (var role in roles)
//        {
//            if (!await roleManager.RoleExistsAsync(role))
//            {
//                await roleManager.CreateAsync(
//                    new ApplicationRole
//                    {
//                        Name = role,
//                        Description = $"{role} Role"
//                    });
//            }
//        }

//        var superAdminEmail =
//            "superadmin@inventory.com";

//        var existingUser =
//            await userManager.FindByEmailAsync(
//                superAdminEmail);

//        if (existingUser == null)
//        {
//            var user =
//                new ApplicationUser
//                {
//                    FullName = "Super Admin",
//                    UserName = "superadmin",
//                    Email = superAdminEmail,
//                    IsActive = true,
//                    EmailConfirmed = true
//                };

//            var result =
//                await userManager.CreateAsync(
//                    user,
//                    "SuperAdmin@123");

//            if (result.Succeeded)
//            {
//                await userManager.AddToRoleAsync(
//                    user,
//                    "SuperAdmin");
//            }
//        }
//    }
//}

using INVENTORYAPP.Models;
using Microsoft.AspNetCore.Identity;

namespace INVENTORYAPP.Infrastructure.Seed;

public static class IdentitySeeder
{
    public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
    {
        string[] roles =
        {
            "SuperAdmin",
            "Admin",
            "Employee",
            "Customer"
        };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(
                    new ApplicationRole
                    {
                        Name = role,
                        Description = $"{role} Role"
                    });
            }
        }

        const string superAdminEmail = "superadmin@inventory.com";
        const string loginPassword = "SuperAdmin@123";
        const string editPassword = "1234";

        var user =
            await userManager.FindByEmailAsync(superAdminEmail);

        if (user == null)
        {
            user = new ApplicationUser
            {
                FullName = "Super Admin",
                UserName = "superadmin",
                Email = superAdminEmail,
                IsActive = true,
                EmailConfirmed = true
            };

            var result =
                await userManager.CreateAsync(
                    user,
                    loginPassword);

            if (!result.Succeeded)
                return;

            await userManager.AddToRoleAsync(
                user,
                "SuperAdmin");
        }

        if (string.IsNullOrWhiteSpace(user.EditPasswordHash))
        {
            var hasher =
                new PasswordHasher<ApplicationUser>();

            user.EditPasswordHash =
                hasher.HashPassword(
                    user,
                    editPassword);

            await userManager.UpdateAsync(user);
        }
    }
}


