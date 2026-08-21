namespace INVENTORYAPP.DTOs.Menu;

public class MenuDto
{
    public int MenuId { get; set; }

    public string MenuName { get; set; } = string.Empty;

    public int? ParentMenuId { get; set; }

    public string? MenuUrl { get; set; }

    public string? Icon { get; set; }

    public int SortOrder { get; set; }
}