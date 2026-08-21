using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace INVENTORYAPP.Shared.Pdf;

public static class PdfFooter
{
    public static void Add(Document document)
    {
        document.Add(new Paragraph(" "));

        var footerTable =
            new Table(UnitValue.CreatePercentArray(new float[] { 50, 50 }))
            .UseAllAvailableWidth();

        //--------------------------------------------------------
        // Left Side
        //--------------------------------------------------------

        footerTable.AddCell(

            PdfHelper.Cell(
                $"Generated : {DateTime.Now:dd-MM-yyyy HH:mm:ss}",
                false)

            .SetBorder(iText.Layout.Borders.Border.NO_BORDER)

        );

        //--------------------------------------------------------
        // Right Side
        //--------------------------------------------------------

        footerTable.AddCell(

            PdfHelper.Cell(
                "INVENTORY ERP",
                true,
                null,
                TextAlignment.RIGHT)

            .SetBorder(iText.Layout.Borders.Border.NO_BORDER)

        );

        document.Add(footerTable);
    }
}