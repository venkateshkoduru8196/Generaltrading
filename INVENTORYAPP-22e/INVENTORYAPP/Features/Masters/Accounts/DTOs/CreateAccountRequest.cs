namespace INVENTORYAPP.Features.Masters.Accounts.DTOs;

public class CreateAccountRequest
{
    public string AccountCode { get; set; } = string.Empty;

    public string AccountName { get; set; } = string.Empty;

    //==========================================
    // Account Type
    //
    // G = General
    // B = Bank/Cash
    // C = Customer
    // S = Supplier
    //==========================================

    public string Actype { get; set; } = "G";
}