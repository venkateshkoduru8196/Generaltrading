namespace INVENTORYAPP.Features.Sales.Interfaces.Export;

public interface IGSalPdfService
{
    Task<byte[]> GeneratePdfAsync(int saleId);
}