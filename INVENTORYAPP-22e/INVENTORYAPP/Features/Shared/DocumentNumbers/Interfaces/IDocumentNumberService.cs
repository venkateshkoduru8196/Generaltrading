namespace INVENTORYAPP.Features.Shared.DocumentNumbers.Interfaces;

public interface IDocumentNumberService
{
    Task<string> GenerateAsync(
        int companyId,
        string moduleCode);
}


