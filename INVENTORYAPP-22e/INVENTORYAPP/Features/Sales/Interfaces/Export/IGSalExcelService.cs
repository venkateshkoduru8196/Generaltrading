namespace INVENTORYAPP.Features.Sales.Interfaces.Export;

public interface IGSalExcelService
{
    //====================================================
    // GENERATE SALES INVOICE EXCEL DOCUMENT
    //====================================================

    Task<byte[]> GenerateExcelAsync(
        int saleId);
}
