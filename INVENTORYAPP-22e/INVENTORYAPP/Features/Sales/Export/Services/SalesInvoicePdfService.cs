using INVENTORYAPP.Features.Sales.Export.Pdf;
using INVENTORYAPP.Features.Sales.Interfaces;
using INVENTORYAPP.Features.Sales.Interfaces.Export;
using INVENTORYAPP.Shared.Pdf;
using iText.Kernel.Geom;

namespace INVENTORYAPP.Features.Sales.Export.Services;

public class SalesInvoicePdfService : IGSalPdfService
{
    private readonly IGSalService _saleService;

    //====================================================
    // Constructor
    //====================================================

    public SalesInvoicePdfService(
        IGSalService saleService)
    {
        _saleService = saleService;
    }

    //====================================================
    // Generate Sales Invoice PDF
    //====================================================

    public async Task<byte[]> GeneratePdfAsync(
        int saleId)
    {
        //------------------------------------------
        // Get Invoice Data
        //------------------------------------------

        var invoice =
            await _saleService.GetInvoiceForExportAsync(
                saleId);

        //------------------------------------------
        // Invoice Not Found
        //------------------------------------------

        if (invoice == null)
            throw new Exception(
                "Invoice not found.");

        //------------------------------------------
        // Memory Stream
        //------------------------------------------

        using var stream =
            new MemoryStream();

        //------------------------------------------
        // Create A4 Portrait Document
        //------------------------------------------

        var document =
            PdfDocumentBuilder.Create(
                stream,
                PageSize.A4);

        //------------------------------------------
        // Build Invoice Layout
        //------------------------------------------

        SalesInvoicePdfLayout.Build(
            document,
            invoice);

        //------------------------------------------
        // Close Document
        //------------------------------------------

        document.Close();

        //------------------------------------------
        // Return PDF Bytes
        //------------------------------------------

        return stream.ToArray();
    }
}


