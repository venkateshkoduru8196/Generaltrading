using INVENTORYAPP.Models;
using INVENTORYAPP.Repositories.Interfaces;
using INVENTORYAPP.Services.Interfaces;

namespace INVENTORYAPP.Services;
using INVENTORYAPP.DTOs.Menu;

public class MenuService : IMenuService
{
    private readonly IMenuRepository _menuRepository;

    public MenuService(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<List<MenuMaster>> GetAllMenusAsync()
    {
        return await _menuRepository.GetAllMenusAsync();
    }

    public async Task<List<MenuMaster>> GetMenusByRoleAsync(string roleId)
    {
        return await _menuRepository.GetMenusByRoleAsync(roleId);
    }

    public async Task AssignMenuAsync(RoleMenu roleMenu)
    {
        await _menuRepository.AssignMenuAsync(roleMenu);
    }

    public async Task<List<MenuTreeDto>>
GetMenuTreeByRoleAsync(
    string roleId)
    {
        var menus =
            await _menuRepository
                .GetMenusByRoleAsync(
                    roleId);

        var parentMenus =
            menus
            .Where(x =>
                x.ParentMenuId == null)
            .OrderBy(x =>
                x.SortOrder)
            .ToList();

        var result =
            new List<MenuTreeDto>();

        foreach (var parent
            in parentMenus)
        {
            var menu =
                new MenuTreeDto
                {
                    MenuId =
                        parent.MenuId,

                    MenuName =
                        parent.MenuName,

                    MenuUrl =
                        parent.MenuUrl,

                    Icon =
                        parent.Icon
                };

            menu.Children =
                menus
                .Where(x =>
                    x.ParentMenuId ==
                    parent.MenuId)

                .OrderBy(x =>
                    x.SortOrder)

                .Select(x =>
                    new MenuTreeDto
                    {
                        MenuId =
                            x.MenuId,

                        MenuName =
                            x.MenuName,

                        MenuUrl =
                            x.MenuUrl,

                        Icon =
                            x.Icon
                    })

                .ToList();

            result.Add(menu);
        }

        return result;
    }

}