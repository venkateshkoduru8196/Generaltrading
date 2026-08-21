using INVENTORYAPP.Data;
using INVENTORYAPP.Infrastructure.Seed;
using INVENTORYAPP.Models;
using INVENTORYAPP.Seed;
using Microsoft.AspNetCore.Identity;

namespace INVENTORYAPP.Extensions;

public static class SeedExtensions
{
    public static async Task SeedDatabaseAsync(
        this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var userManager =
            scope.ServiceProvider
                .GetRequiredService<UserManager<ApplicationUser>>();

        var roleManager =
            scope.ServiceProvider
                .GetRequiredService<RoleManager<ApplicationRole>>();

        await IdentitySeeder.SeedAsync(
            userManager,
            roleManager);

        var context =
            scope.ServiceProvider
                .GetRequiredService<AppDbContext>();

        await MenuSeeder.SeedMenusAsync(context);
    }
}