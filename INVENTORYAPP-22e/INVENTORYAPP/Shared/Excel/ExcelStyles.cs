using OfficeOpenXml.Style;
using System.Drawing;

namespace INVENTORYAPP.Shared.Excel;

public static class ExcelStyles
{
    //====================================================
    // PRIMARY
    // Same as PDF / Word
    // #1976D2
    //====================================================

    public static readonly Color Primary =
        Color.FromArgb(25, 118, 210);


    //====================================================
    // TITLE BACKGROUND
    // Same as Primary
    // #1976D2
    //====================================================

    public static readonly Color TitleBackground =
        Color.FromArgb(25, 118, 210);


    //====================================================
    // SECONDARY
    // Same as PDF / Word
    // #0D47A1
    //====================================================

    public static readonly Color Secondary =
        Color.FromArgb(13, 71, 161);


    //====================================================
    // TABLE HEADER
    // Same as PDF / Word
    // #2196F3
    //====================================================

    public static readonly Color HeaderBackground =
        Color.FromArgb(33, 150, 243);


    //====================================================
    // ALTERNATE ROW
    // Same as PDF / Word
    // #F5F5F5
    //====================================================

    public static readonly Color AlternateRow =
        Color.FromArgb(245, 245, 245);


    //====================================================
    // TOTAL ROW
    // Same as PDF / Word
    // #E8F5E9
    //====================================================

    public static readonly Color TotalBackground =
        Color.FromArgb(232, 245, 233);


    //====================================================
    // BORDER
    // Same as PDF / Word
    // #C8C8C8
    //====================================================

    public static readonly Color BorderColor =
        Color.FromArgb(200, 200, 200);


    //====================================================
    // TEXT
    //====================================================

    public static readonly Color Black =
        Color.FromArgb(0, 0, 0);

    public static readonly Color White =
        Color.FromArgb(255, 255, 255);


    //====================================================
    // STATUS
    //====================================================

    public static readonly Color Red =
        Color.FromArgb(220, 53, 69);

    public static readonly Color Green =
        Color.FromArgb(40, 167, 69);


    //====================================================
    // BORDER STYLE
    //====================================================

    public const ExcelBorderStyle Border =
        ExcelBorderStyle.Thin;
}