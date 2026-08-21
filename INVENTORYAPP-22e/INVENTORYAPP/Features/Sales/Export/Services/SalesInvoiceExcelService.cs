using INVENTORYAPP.Features.Sales.Export.Excel;
using INVENTORYAPP.Features.Sales.Interfaces;
using INVENTORYAPP.Features.Sales.Interfaces.Export;
using INVENTORYAPP.Shared.Excel;

namespace INVENTORYAPP.Features.Sales.Export.Services;

public class SalesInvoiceExcelService : IGSalExcelService
{
    private readonly IGSalService _saleService;

    //====================================================
    // CONSTRUCTOR
    //====================================================

    public SalesInvoiceExcelService(
        IGSalService saleService)
    {
        _saleService = saleService;
    }


    //====================================================
    // GENERATE SALES INVOICE EXCEL
    //====================================================

    public async Task<byte[]> GenerateExcelAsync(
        int saleId)
    {
        //================================================
        // 1. GET INVOICE DATA
        //================================================

        var invoice =
            await _saleService
                .GetInvoiceForExportAsync(saleId);

        //================================================
        // 2. VALIDATE INVOICE
        //================================================

        if (invoice == null)
        {
            throw new KeyNotFoundException(
                $"Sales invoice with ID {saleId} was not found.");
        }

        //================================================
        // 3. CREATE EXCEL WORKBOOK
        //================================================

        using var package =
            ExcelDocumentBuilder.Create(
                "Sales Invoice",
                $"Sales Invoice - {invoice.InvoiceNo}");

        //================================================
        // 4. GET WORKSHEET
        //================================================

        var sheet =
            package.Workbook.Worksheets[
                "Sales Invoice"];

        //================================================
        // 5. BUILD INVOICE LAYOUT
        //================================================

        SalesInvoiceExcelLayout.Build(
            sheet,
            invoice);

        //================================================
        // 6. RETURN EXCEL BYTES
        //================================================

        return await package
            .GetAsByteArrayAsync();
    }
}


