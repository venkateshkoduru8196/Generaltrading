namespace INVENTORYAPP.DTOs.Menu;

public class MenuTreeDto
{
    public int MenuId { get; set; }

    public string MenuName { get; set; }
        = string.Empty;

    public string? MenuUrl { get; set; }

    public string? Icon { get; set; }

    public List<MenuTreeDto> Children
    { get; set; }
        = new();
}