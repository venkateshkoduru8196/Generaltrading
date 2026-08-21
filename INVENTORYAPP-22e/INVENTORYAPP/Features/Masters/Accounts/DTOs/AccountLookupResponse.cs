namespace INVENTORYAPP.Features.Masters.Accounts.DTOs;

public class AccountLookupResponse
{
    public int Id { get; set; }

    public string AccountCode { get; set; } = string.Empty;

    public string AccountName { get; set; } = string.Empty;

    public string Actype { get; set; } = "G";
}

