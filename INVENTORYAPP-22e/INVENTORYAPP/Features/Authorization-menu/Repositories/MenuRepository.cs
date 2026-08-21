using INVENTORYAPP.Data;
using INVENTORYAPP.Models;
using INVENTORYAPP.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Repositories;

public class MenuRepository : IMenuRepository
{
    private readonly AppDbContext _context;

    public MenuRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<MenuMaster>> GetAllMenusAsync()
    {
        return await _context.MenuMasters
            .OrderBy(x => x.SortOrder)
            .ToListAsync();
    }

    public async Task<List<MenuMaster>> GetMenusByRoleAsync(string roleId)
    {
        return await _context.RoleMenus
            .Where(x => x.RoleId == roleId)
            .Select(x => x.Menu!)
            .OrderBy(x => x.SortOrder)
            .ToListAsync();
    }

    public async Task AssignMenuAsync(RoleMenu roleMenu)
    {
        _context.RoleMenus.Add(roleMenu);

        await _context.SaveChangesAsync();
    }
}
