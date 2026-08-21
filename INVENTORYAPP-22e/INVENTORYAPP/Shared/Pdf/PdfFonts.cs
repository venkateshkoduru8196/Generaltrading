using iText.IO.Font.Constants;
using iText.Kernel.Font;

namespace INVENTORYAPP.Shared.Pdf;

public static class PdfFonts
{
    public static PdfFont Regular =>
        PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

    public static PdfFont Bold =>
        PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

    public static PdfFont Italic =>
        PdfFontFactory.CreateFont(StandardFonts.HELVETICA_OBLIQUE);

    public static PdfFont BoldItalic =>
        PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLDOBLIQUE);
}