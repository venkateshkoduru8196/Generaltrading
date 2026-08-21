using Xceed.Document.NET;
using Xceed.Words.NET;

namespace INVENTORYAPP.Shared.Word;

public static class WordHelper
{
    //--------------------------------------------------------
    // Company Name
    //--------------------------------------------------------

    public static void AddCompany(
        DocX document)
    {
        var p = document.InsertParagraph();

        p.Append(WordTheme.CompanyName)
         .Bold()
         .FontSize(WordTheme.CompanyFontSize);

        p.Alignment =
            Alignment.center;
    }

    //--------------------------------------------------------
    // Report Title
    //--------------------------------------------------------

    public static void AddTitle(
        DocX document,
        string title)
    {
        var p = document.InsertParagraph();

        p.Append(title)
         .Bold()
         .FontSize(WordTheme.TitleFontSize);

        p.Alignment =
            Alignment.center;
    }

    //--------------------------------------------------------
    // Normal Paragraph
    //--------------------------------------------------------

    public static void AddParagraph(
        DocX document,
        string text)
    {
        document.InsertParagraph(text)
                .FontSize(WordTheme.BodyFontSize);
    }

    //--------------------------------------------------------
    // Heading
    //--------------------------------------------------------

    public static void AddHeading(
        DocX document,
        string text)
    {
        document.InsertParagraph(text)
                .Bold()
                .FontSize(WordTheme.HeaderFontSize);
    }

    //--------------------------------------------------------
    // Empty Line
    //--------------------------------------------------------

    public static void AddBlankLine(
        DocX document)
    {
        document.InsertParagraph();
    }
}