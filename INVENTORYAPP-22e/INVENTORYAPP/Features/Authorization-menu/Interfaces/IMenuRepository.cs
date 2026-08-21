using INVENTORYAPP.Models;

namespace INVENTORYAPP.Repositories.Interfaces;

public interface IMenuRepository
{
    Task<List<MenuMaster>> GetAllMenusAsync();

    Task<List<MenuMaster>> GetMenusByRoleAsync(string roleId);

    Task AssignMenuAsync(RoleMenu roleMenu);
}
