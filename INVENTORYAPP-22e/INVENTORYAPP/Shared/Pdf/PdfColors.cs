using iText.Kernel.Colors;

namespace INVENTORYAPP.Shared.Pdf;

public static class PdfColors
{
    // Theme Colors
    public static readonly DeviceRgb Primary =
        new DeviceRgb(25, 118, 210);

    public static readonly DeviceRgb Secondary =
        new DeviceRgb(13, 71, 161);

    // Table Header
    public static readonly DeviceRgb Header =
        new DeviceRgb(33, 150, 243);

    // Alternate Row
    public static readonly DeviceRgb AlternateRow =
        new DeviceRgb(245, 245, 245);

    // Total Row
    public static readonly DeviceRgb TotalRow =
        new DeviceRgb(232, 245, 233);

    // Borders
    public static readonly DeviceRgb Border =
        new DeviceRgb(200, 200, 200);

    // Text
    public static readonly DeviceRgb Black =
        new DeviceRgb(0, 0, 0);

    public static readonly DeviceRgb White =
        new DeviceRgb(255, 255, 255);

    public static readonly DeviceRgb Red =
        new DeviceRgb(220, 53, 69);

    public static readonly DeviceRgb Green =
        new DeviceRgb(40, 167, 69);
}