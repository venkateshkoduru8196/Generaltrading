namespace INVENTORYAPP.Features.Companies.DTOs;
public class CreateCompanyRequest
{
    public string CompanyCode { get; set; } = string.Empty;

    public string CompanyName { get; set; } = string.Empty;

    public string OwnerName { get; set; } = string.Empty;

    public string GSTIN { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;
}