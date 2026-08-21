namespace INVENTORYAPP.Features.Masters.Items.DTOs;

public class ItemCreateDto
{
    public string? Code { get; set; }

    public string? Name { get; set; }

    public string? RegionalName { get; set; }

    public string? HsnCode { get; set; }

    public decimal? CgstPer { get; set; }

    public decimal? SgstPer { get; set; }

    public decimal? IgstPer { get; set; }

    public decimal? PRate { get; set; }

    public decimal? SRate { get; set; }

    public decimal? Mrp { get; set; }

    public bool? IsExpiry { get; set; }

    public string? Remarks { get; set; }
}