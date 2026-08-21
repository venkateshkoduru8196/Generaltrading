using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace INVENTORYAPP.Shared.Excel;

public static class ExcelHelper
{
    //--------------------------------------------------------
    // Title
    //--------------------------------------------------------

    public static void AddTitle(
        ExcelWorksheet sheet,
        int row,
        int fromCol,
        int toCol,
        string text)
    {
        var cell = sheet.Cells[row, fromCol, row, toCol];

        cell.Merge = true;
        cell.Value = text;

        cell.Style.Font.Bold = true;
        cell.Style.Font.Size = ExcelTheme.TitleFontSize;

        cell.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Center;

        cell.Style.Fill.PatternType =
            ExcelFillStyle.Solid;

        cell.Style.Fill.BackgroundColor
            .SetColor(ExcelStyles.TitleBackground);

        cell.Style.Font.Color
            .SetColor(ExcelStyles.White);
    }

    //--------------------------------------------------------
    // Header
    //--------------------------------------------------------

    public static void AddHeader(
        ExcelWorksheet sheet,
        int row,
        int column,
        string text)
    {
        var cell = sheet.Cells[row, column];

        cell.Value = text;

        cell.Style.Font.Bold = true;

        cell.Style.Fill.PatternType =
            ExcelFillStyle.Solid;

        cell.Style.Fill.BackgroundColor
            .SetColor(ExcelStyles.HeaderBackground);

        cell.Style.Font.Color
            .SetColor(ExcelStyles.White);

        cell.Style.Border.BorderAround(
            ExcelStyles.Border);
    }

    //--------------------------------------------------------
    // Cell
    //--------------------------------------------------------

    public static void AddCell(
        ExcelWorksheet sheet,
        int row,
        int column,
        object value)
    {
        var cell = sheet.Cells[row, column];

        cell.Value = value;

        cell.Style.Border.BorderAround(
            ExcelStyles.Border);
    }

    //--------------------------------------------------------
    // Auto Fit
    //--------------------------------------------------------

    public static void AutoFit(
        ExcelWorksheet sheet)
    {
        sheet.Cells.AutoFitColumns();
    }
}