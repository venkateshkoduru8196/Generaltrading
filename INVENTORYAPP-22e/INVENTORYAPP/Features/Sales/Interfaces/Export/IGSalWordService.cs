namespace INVENTORYAPP.Features.Sales.Interfaces.Export;

public interface IGSalWordService
{
    //====================================================
    // GENERATE SALES INVOICE WORD DOCUMENT
    //====================================================

    Task<byte[]> GenerateWordAsync(
        int saleId);
}

