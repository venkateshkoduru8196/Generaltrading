using System.ComponentModel.DataAnnotations;

namespace INVENTORYAPP.Models;

public class MetalMaster
{
    [Key]
    public string StkCode { get; set; } = string.Empty;

    public string? StkName { get; set; }

    public string? MtlType { get; set; }

    public string? Karat { get; set; }

    public decimal? Purity { get; set; }

    public decimal? SPurity { get; set; }

    public string? KaratCat { get; set; }

    public string? Cat { get; set; }

    public short? IsPcs { get; set; }

    public short? IsWeight { get; set; }

    public string? Uom { get; set; }

    public string? LccCode { get; set; }

    public string? MkgLccCode { get; set; }
}
