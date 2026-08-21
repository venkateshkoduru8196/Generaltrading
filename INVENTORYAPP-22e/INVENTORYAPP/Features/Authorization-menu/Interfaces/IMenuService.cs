using INVENTORYAPP.DTOs.Menu;
using INVENTORYAPP.Models;

namespace INVENTORYAPP.Services.Interfaces;

public interface IMenuService
{
    Task<List<MenuMaster>> GetAllMenusAsync();

    Task<List<MenuMaster>> GetMenusByRoleAsync(string roleId);

    Task AssignMenuAsync(RoleMenu roleMenu);

    Task<List<MenuTreeDto>>
GetMenuTreeByRoleAsync(
    string roleId);
}
