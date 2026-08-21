using iText.Layout.Element;
using iText.Layout.Properties;

namespace INVENTORYAPP.Shared.Pdf;

public static class PdfTable
{
    //----------------------------------------------------------
    // Create Table
    //----------------------------------------------------------

    public static Table Create(params float[] widths)
    {
        return new Table(UnitValue.CreatePercentArray(widths))
            .UseAllAvailableWidth();
    }

    //----------------------------------------------------------
    // Add Header Row
    //----------------------------------------------------------

    public static void AddHeader(
        Table table,
        params string[] headers)
    {
        foreach (var header in headers)
        {
            table.AddHeaderCell(

                PdfHelper.Cell(
                    header,
                    bold: true,
                    background: PdfColors.Header,
                    alignment: TextAlignment.CENTER)

                .SetFontColor(PdfColors.White)

            );
        }
    }

    //----------------------------------------------------------
    // Add Text Cell
    //----------------------------------------------------------

    public static void AddText(
        Table table,
        string value,
        bool alternate = false)
    {
        table.AddCell(

            PdfHelper.Cell(
                value,
                false,
                alternate ? PdfColors.AlternateRow : null)

        );
    }

    //----------------------------------------------------------
    // Add Number Cell
    //----------------------------------------------------------

    public static void AddNumber(
        Table table,
        decimal value,
        bool alternate = false)
    {
        table.AddCell(

            PdfHelper.NumberCell(
                value,
                false,
                alternate ? PdfColors.AlternateRow : null)

        );
    }



    //----------------------------------------------------------
    // Add Total Cell
    //----------------------------------------------------------

    public static void AddTotal(
        Table table,
        string value)
    {
        table.AddCell(

            PdfHelper.Cell(
                value,
                true,
                PdfColors.TotalRow)

        );
    }

    //----------------------------------------------------------
    // Add Total Number
    //----------------------------------------------------------

    public static void AddTotal(
        Table table,
        decimal value)
    {
        table.AddCell(

            PdfHelper.NumberCell(
                value,
                true,
                PdfColors.TotalRow)

        );
    }
}