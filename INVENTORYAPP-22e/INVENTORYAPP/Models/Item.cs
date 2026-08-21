namespace INVENTORYAPP.Models;

public class Item
{
    public long Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public long? CatId { get; set; }

    public decimal? CgstPer { get; set; }

    public decimal? SgstPer { get; set; }

    public bool? STaxIncl { get; set; }

    public bool? PTaxIncl { get; set; }

    public long? ManufId { get; set; }

    public long? MainUnit { get; set; }

    public string? Rack { get; set; }

    public decimal? PRate { get; set; }

    public decimal? SRate { get; set; }

    public decimal? Mrp { get; set; }

    public bool? Active { get; set; }

    public decimal? IgstPer { get; set; }

    public double? Rol { get; set; }

    public string? Remarks { get; set; }

    public string? RegionalName { get; set; }

    public bool? IsExpiry { get; set; }

    public string? HsnCode { get; set; }

    public string? DefBarcode { get; set; }

    public bool? Deactivate { get; set; }

    public double? CessPer { get; set; }

    public double? AddCess { get; set; }
}
