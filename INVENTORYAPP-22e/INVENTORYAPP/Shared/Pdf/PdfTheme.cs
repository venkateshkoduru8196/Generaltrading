using iText.Kernel.Geom;
using iText.Layout.Borders;

namespace INVENTORYAPP.Shared.Pdf;

public static class PdfTheme
{
    //--------------------------------------------------------
    // Page
    //--------------------------------------------------------

    public static readonly PageSize PageSize =
        PageSize.A4.Rotate();

    public const float MarginLeft = 20;

    public const float MarginRight = 20;

    public const float MarginTop = 25;

    public const float MarginBottom = 20;

    //--------------------------------------------------------
    // Fonts
    //--------------------------------------------------------

    public const float TitleFont = 20;

    public const float HeaderFont = 11;

    public const float BodyFont = 10;

    public const float SmallFont = 9;

    //--------------------------------------------------------
    // Table
    //--------------------------------------------------------

    public const float CellPadding = 6;

    public static readonly Border TableBorder =
        new SolidBorder(PdfColors.Border, 0.5f);

    //--------------------------------------------------------
    // Company
    //--------------------------------------------------------

    public const string CompanyName =
        "INVENTORY ERP";

    public const string CompanyAddress =
        "Business Management System";
}