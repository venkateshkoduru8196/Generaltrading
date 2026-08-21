using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;

namespace INVENTORYAPP.Shared.Pdf;

public static class PdfDocumentBuilder
{
    //========================================================
    // Create PDF Document
    //========================================================

    public static Document Create(
        Stream stream,
        PageSize? pageSize = null)
    {
        //----------------------------------------------------
        // Writer
        //----------------------------------------------------

        var writer = new PdfWriter(stream);

        //----------------------------------------------------
        // PDF
        //----------------------------------------------------

        var pdf = new PdfDocument(writer);

        //----------------------------------------------------
        // Page Size
        //----------------------------------------------------

        var selectedPageSize =
            pageSize ?? PdfTheme.PageSize;

        //----------------------------------------------------
        // Document
        //----------------------------------------------------

        var document =
            new Document(
                pdf,
                selectedPageSize);

        //----------------------------------------------------
        // Margins
        //----------------------------------------------------

        document.SetMargins(
            PdfTheme.MarginTop,
            PdfTheme.MarginRight,
            PdfTheme.MarginBottom,
            PdfTheme.MarginLeft);

        //----------------------------------------------------
        // Return
        //----------------------------------------------------

        return document;
    }
}