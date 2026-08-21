namespace INVENTORYAPP.Features.Companies.DTOs;

public class CompanyLookupResponse
{
    public int CompanyId { get; set; }

    public string CompanyCode { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;
}