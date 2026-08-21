namespace INVENTORYAPP.Models;

public class RoleMenu
{
    public int RoleMenuId { get; set; }

    public string RoleId { get; set; } = string.Empty;

    public int MenuId { get; set; }

    public ApplicationRole? Role { get; set; }

    public MenuMaster? Menu { get; set; }
}