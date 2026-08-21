
using INVENTORYAPP.DTOs.Menu;
using INVENTORYAPP.Models;
using INVENTORYAPP.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class MenuController : ControllerBase
{
    private readonly IMenuService _menuService;

    public MenuController(IMenuService menuService)
    {
        _menuService = menuService;
    }

    // ==========================
    // Get All Menus
    // ==========================
    [HttpGet]
    public async Task<IActionResult> GetAllMenus()
    {
        var menus =
            await _menuService.GetAllMenusAsync();

        return Ok(menus);
    }

    // ==========================
    // Get Menus By Role
    // ==========================
    [HttpGet("role/{roleId}")]
    public async Task<IActionResult> GetMenusByRole(
        string roleId)
    {
        var menus =
            await _menuService.GetMenusByRoleAsync(roleId);

        return Ok(menus);
    }

    // ==========================
    // Assign Menu To Role
    // ==========================
    [HttpPost("assign")]
    public async Task<IActionResult> AssignMenu(
        AssignMenuDto dto)
    {
        var roleMenu = new RoleMenu
        {
            RoleId = dto.RoleId,
            MenuId = dto.MenuId
        };

        await _menuService.AssignMenuAsync(roleMenu);

        return Ok("Menu Assigned Successfully");
    }


    [HttpGet("tree/{roleId}")]
    public async Task<IActionResult>
GetMenuTree(string roleId)
    {
        var menus =
            await _menuService
                .GetMenuTreeByRoleAsync(
                    roleId);

        return Ok(menus);
    }

}