using INVENTORYAPP.Data;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Seed;

public static class MenuSeeder
{
    public static async Task SeedMenusAsync(AppDbContext context)
    {
        if (await context.MenuMasters.AnyAsync())
            return;

        var menus = new List<MenuMaster>
        {
            new() { MenuName = "Dashboard", SortOrder = 1 },

            new() { MenuName = "Master", SortOrder = 2 },
            new() { MenuName = "Purchase", SortOrder = 3 },
            new() { MenuName = "Sales", SortOrder = 4 },
            new() { MenuName = "Inventory", SortOrder = 5 },
            new() { MenuName = "Accounts", SortOrder = 6 },
            new() { MenuName = "GST Reports", SortOrder = 7 },
            new() { MenuName = "Reports", SortOrder = 8 },
            new() { MenuName = "CRM", SortOrder = 9 },
            new() { MenuName = "User Management", SortOrder = 10 },
            new() { MenuName = "Settings", SortOrder = 11 }
        };

        context.MenuMasters.AddRange(menus);

        await context.SaveChangesAsync();
    }
}