public class GSalDetailRequestDto
{
    public int SlNo { get; set; }

    public string StockCode { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string UnitCode { get; set; } = string.Empty;

    public decimal Qty { get; set; }

    public decimal Rate { get; set; }

    public decimal TaxRate { get; set; }
}