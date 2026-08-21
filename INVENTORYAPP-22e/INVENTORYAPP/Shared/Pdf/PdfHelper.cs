using iText.Kernel.Colors;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace INVENTORYAPP.Shared.Pdf;

public static class PdfHelper
{
    //----------------------------------------------------------
    // Paragraph
    //----------------------------------------------------------

    public static Paragraph Paragraph(
        string text,
        float fontSize = PdfTheme.BodyFont,
        bool bold = false,
        Color? color = null)
    {
        var paragraph = new Paragraph(text)
            .SetFont(bold ? PdfFonts.Bold : PdfFonts.Regular)
            .SetFontSize(fontSize);

        if (color != null)
            paragraph.SetFontColor(color);

        return paragraph;
    }

    //----------------------------------------------------------
    // Text Cell
    //----------------------------------------------------------

    public static Cell Cell(
        string text,
        bool bold = false,
        Color? background = null,
        TextAlignment alignment = TextAlignment.LEFT)
    {
        var cell = new Cell();

        cell.SetBorder(PdfTheme.TableBorder);
        cell.SetPadding(PdfTheme.CellPadding);
        cell.SetTextAlignment(alignment);

        if (background != null)
            cell.SetBackgroundColor(background);

        cell.Add(
            Paragraph(
                text,
                PdfTheme.BodyFont,
                bold));

        return cell;
    }

    //----------------------------------------------------------
    // Number Cell
    //----------------------------------------------------------

    public static Cell NumberCell(
        decimal value,
        bool bold = false,
        Color? background = null)
    {
        return Cell(
            value.ToString("N2"),
            bold,
            background,
            TextAlignment.RIGHT);
    }

    //----------------------------------------------------------
    // Date Cell
    //----------------------------------------------------------

    public static Cell DateCell(
        DateTime date,
        bool bold = false,
        Color? background = null)
    {
        return Cell(
            date.ToString("dd-MM-yyyy"),
            bold,
            background,
            TextAlignment.CENTER);
    }

    //----------------------------------------------------------
    // Empty Cell
    //----------------------------------------------------------

    public static Cell EmptyCell()
    {
        return Cell(string.Empty);
    }
}