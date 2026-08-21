using INVENTORYAPP.Features.Sales.Interfaces;
using INVENTORYAPP.Features.Sales.Interfaces.Export;
using INVENTORYAPP.Features.Sales.Export.Word;
using Xceed.Words.NET;

namespace INVENTORYAPP.Features.Sales.Export.Services;

public class SalesInvoiceWordService : IGSalWordService
{
    private readonly IGSalService _saleService;

    public SalesInvoiceWordService(
        IGSalService saleService)
    {
        _saleService = saleService;
    }

    //====================================================
    // GENERATE SALES INVOICE WORD
    //====================================================

    public async Task<byte[]> GenerateWordAsync(
        int saleId)
    {
        //------------------------------------------
        // Get Invoice Data
        //------------------------------------------

        var invoice =
            await _saleService.GetInvoiceForExportAsync(
                saleId);

        //------------------------------------------
        // Validate Invoice
        //------------------------------------------

        if (invoice == null)
        {
            throw new KeyNotFoundException(
                $"Sales invoice with ID {saleId} was not found.");
        }

        //------------------------------------------
        // Memory Stream
        //------------------------------------------

        using var stream =
            new MemoryStream();

        //------------------------------------------
        // Create Word Document
        //------------------------------------------

        using var document =
            DocX.Create(stream);

        //------------------------------------------
        // Build Invoice Layout
        //------------------------------------------

        SalesInvoiceWordLayout.Build(
            document,
            invoice);

        //------------------------------------------
        // Save Document
        //------------------------------------------

        document.Save();

        //------------------------------------------
        // Return DOCX
        //------------------------------------------

        return stream.ToArray();
    }
}