using INVENTORYAPP.Features.Sales.Export.DTOs;
using INVENTORYAPP.Shared.Excel;

using OfficeOpenXml;
using OfficeOpenXml.Style;

using SharedExcelStyles = INVENTORYAPP.Shared.Excel.ExcelStyles;

namespace INVENTORYAPP.Features.Sales.Export.Excel;

public static class SalesInvoiceExcelLayout
{
    //====================================================
    // BUILD
    //====================================================

    public static void Build(
        ExcelWorksheet sheet,
        SalesInvoiceExportDto invoice)
    {
        ConfigureWorksheet(sheet);

        int row = AddCompanyHeader(
            sheet,
            invoice);

        row = AddInvoiceInformation(
            sheet,
            invoice,
            row);

        int itemHeaderRow = row;

        row = AddItemsTable(
            sheet,
            invoice,
            row);

        row = AddGrandTotal(
            sheet,
            invoice,
            row);

        row = AddSignatures(
            sheet,
            row);

        AddFooter(
            sheet,
            row);

        ConfigurePrintArea(
            sheet,
            row,
            itemHeaderRow);
    }


    //====================================================
    // WORKSHEET
    //====================================================

    private static void ConfigureWorksheet(
        ExcelWorksheet sheet)
    {
        sheet.Cells.Style.Font.Name =
            ExcelTheme.FontName;

        sheet.Cells.Style.Font.Size =
            ExcelTheme.BodyFontSize;

        //================================================
        // COLUMN WIDTHS
        //================================================

        sheet.Column(1).Width = 7;
        sheet.Column(2).Width = 32;
        sheet.Column(3).Width = 14;
        sheet.Column(4).Width = 12;
        sheet.Column(5).Width = 14;
        sheet.Column(6).Width = 16;
        sheet.Column(7).Width = 12;
        sheet.Column(8).Width = 16;

        //================================================
        // VIEW
        //================================================

        sheet.View.ShowGridLines = false;

        //================================================
        // FREEZE ITEM HEADER
        //================================================

        sheet.View.FreezePanes(1, 1);

        //================================================
        // PAGE SETUP
        //================================================

        sheet.PrinterSettings.Orientation =
            eOrientation.Landscape;

        sheet.PrinterSettings.PaperSize =
            ePaperSize.A4;

        sheet.PrinterSettings.FitToPage =
            true;

        sheet.PrinterSettings.FitToWidth =
            1;

        sheet.PrinterSettings.FitToHeight =
            0;

        //================================================
        // MARGINS
        //================================================

        sheet.PrinterSettings.TopMargin =
            0.35;

        sheet.PrinterSettings.BottomMargin =
            0.35;

        sheet.PrinterSettings.LeftMargin =
            0.35;

        sheet.PrinterSettings.RightMargin =
            0.35;

        sheet.PrinterSettings.HeaderMargin =
            0.15;

        sheet.PrinterSettings.FooterMargin =
            0.15;

        sheet.PrinterSettings.HorizontalCentered =
            true;

        sheet.PrinterSettings.VerticalCentered =
            false;
    }


    //====================================================
    // COMPANY HEADER
    //====================================================

    private static int AddCompanyHeader(
        ExcelWorksheet sheet,
        SalesInvoiceExportDto invoice)
    {
        int row = 1;

        //================================================
        // COMPANY NAME
        //================================================

        sheet.Cells[row, 1, row, 8].Merge = true;

        var companyName =
            sheet.Cells[row, 1];

        companyName.Value =
            invoice.CompanyName;

        companyName.Style.Font.Name =
            ExcelTheme.FontName;

        companyName.Style.Font.Size =
            ExcelTheme.CompanyFontSize;

        companyName.Style.Font.Bold =
            true;

        companyName.Style.Font.Color
            .SetColor(
                SharedExcelStyles.Primary);

        companyName.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Left;

        companyName.Style.VerticalAlignment =
            ExcelVerticalAlignment.Center;

        sheet.Row(row).Height =
            ExcelTheme.CompanyRowHeight;

        row++;


        //================================================
        // COMPANY ADDRESS
        //================================================

        sheet.Cells[row, 1, row, 8].Merge = true;

        var address =
            sheet.Cells[row, 1];

        address.Value =
            invoice.CompanyAddress;

        address.Style.Font.Name =
            ExcelTheme.FontName;

        address.Style.Font.Size =
            ExcelTheme.BodyFontSize;

        address.Style.Font.Color
            .SetColor(
                SharedExcelStyles.Black);

        address.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Left;

        row++;


        //================================================
        // PHONE + EMAIL
        //================================================

        sheet.Cells[row, 1, row, 8].Merge = true;

        var contact =
            sheet.Cells[row, 1];

        contact.Value =
            $"Phone : {invoice.CompanyPhone}    |    " +
            $"Email : {invoice.CompanyEmail}";

        contact.Style.Font.Name =
            ExcelTheme.FontName;

        contact.Style.Font.Size =
            ExcelTheme.SmallFontSize;

        contact.Style.Font.Color
            .SetColor(
                SharedExcelStyles.Black);

        contact.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Left;

        row++;


        //================================================
        // GSTIN
        //================================================

        sheet.Cells[row, 1, row, 8].Merge = true;

        var gst =
            sheet.Cells[row, 1];

        gst.Value =
            $"GSTIN : {invoice.CompanyGSTIN}";

        gst.Style.Font.Name =
            ExcelTheme.FontName;

        gst.Style.Font.Size =
            ExcelTheme.SmallFontSize;

        gst.Style.Font.Bold =
            true;

        gst.Style.Font.Color
            .SetColor(
                SharedExcelStyles.Black);

        gst.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Left;

        row++;


        //================================================
        // SEPARATOR
        //================================================

        sheet.Cells[row, 1, row, 8].Merge = true;

        var separator =
            sheet.Cells[row, 1];

        separator.Style.Border.Bottom.Style =
            ExcelBorderStyle.Medium;

        separator.Style.Border.Bottom.Color
            .SetColor(
                SharedExcelStyles.Primary);

        sheet.Row(row).Height = 4;

        row += 2;


        //================================================
        // TITLE
        //================================================

        sheet.Cells[row, 1, row, 8].Merge = true;

        var title =
            sheet.Cells[row, 1];

        title.Value =
            "SALES INVOICE";

        title.Style.Font.Name =
            ExcelTheme.FontName;

        title.Style.Font.Size =
            ExcelTheme.TitleFontSize;

        title.Style.Font.Bold =
            true;

        title.Style.Font.Color
            .SetColor(
                SharedExcelStyles.Primary);

        title.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Center;

        title.Style.VerticalAlignment =
            ExcelVerticalAlignment.Center;

        sheet.Row(row).Height =
            ExcelTheme.TitleRowHeight;

        row += 2;

        return row;
    }


    //====================================================
    // INVOICE INFORMATION
    //====================================================

    private static int AddInvoiceInformation(
        ExcelWorksheet sheet,
        SalesInvoiceExportDto invoice,
        int row)
    {
        //================================================
        // SECTION HEADERS
        //================================================

        sheet.Cells[row, 1, row, 4].Merge = true;
        sheet.Cells[row, 5, row, 8].Merge = true;

        StyleSectionHeader(
            sheet.Cells[row, 1],
            "CUSTOMER DETAILS");

        StyleSectionHeader(
            sheet.Cells[row, 5],
            "INVOICE DETAILS");

        sheet.Row(row).Height =
            ExcelTheme.SectionRowHeight;

        row++;


        //================================================
        // CUSTOMER
        //================================================

        AddLabelValue(
            sheet,
            row,
            1,
            "Party Name",
            invoice.PartyName);

        AddLabelValue(
            sheet,
            row + 1,
            1,
            "Party Code",
            invoice.PartyCode);


        //================================================
        // INVOICE
        //================================================

        AddLabelValue(
            sheet,
            row,
            5,
            "Invoice No",
            invoice.InvoiceNo);

        AddLabelValue(
            sheet,
            row + 1,
            5,
            "Invoice Date",
            invoice.InvoiceDate.ToString(
                ExcelTheme.DateFormat));

        AddLabelValue(
            sheet,
            row + 2,
            5,
            "Generated By",
            invoice.GeneratedBy);

        AddLabelValue(
            sheet,
            row + 3,
            5,
            "Generated On",
            invoice.GeneratedOn.ToString(
                ExcelTheme.DateTimeFormat));


        row += 4;

        row++;

        return row;
    }


    //====================================================
    // ITEMS TABLE
    //====================================================

    private static int AddItemsTable(
        ExcelWorksheet sheet,
        SalesInvoiceExportDto invoice,
        int row)
    {
        //================================================
        // HEADER
        //================================================

        string[] headers =
        {
            "SL",
            "ITEM DETAILS",
            "UNIT",
            "QTY",
            "RATE",
            "AMOUNT",
            "TAX %",
            "TAX AMOUNT"
        };

        for (int column = 1;
             column <= headers.Length;
             column++)
        {
            var cell =
                sheet.Cells[row, column];

            cell.Value =
                headers[column - 1];

            StyleTableHeader(cell);
        }

        sheet.Row(row).Height =
            ExcelTheme.HeaderRowHeight;

        row++;


        //================================================
        // DATA
        //================================================

        foreach (var item in invoice.Items)
        {
            sheet.Cells[row, 1].Value =
                item.SlNo;

            sheet.Cells[row, 2].Value =
                string.IsNullOrWhiteSpace(
                    item.Description)
                    ? $"{item.StockCode} - {item.StockName}"
                    : $"{item.StockCode} - {item.StockName}\n" +
                      item.Description;

            sheet.Cells[row, 3].Value =
                item.Unit;

            sheet.Cells[row, 4].Value =
                Convert.ToDouble(item.Qty);

            sheet.Cells[row, 5].Value =
                Convert.ToDouble(item.Rate);

            sheet.Cells[row, 6].Value =
                Convert.ToDouble(item.Amount);

            sheet.Cells[row, 7].Value =
                Convert.ToDouble(item.TaxRate);

            sheet.Cells[row, 8].Value =
                Convert.ToDouble(item.TaxAmount);


            //================================================
            // NUMBER FORMATS
            //================================================

            sheet.Cells[row, 4]
                .Style.Numberformat.Format =
                "#,##0.00";

            sheet.Cells[row, 5]
                .Style.Numberformat.Format =
                "₹ #,##0.00";

            sheet.Cells[row, 6]
                .Style.Numberformat.Format =
                "₹ #,##0.00";

            sheet.Cells[row, 7]
                .Style.Numberformat.Format =
                "0.00";

            sheet.Cells[row, 8]
                .Style.Numberformat.Format =
                "₹ #,##0.00";


            //================================================
            // ROW STYLE
            //================================================

            StyleDataRow(
                sheet,
                row);


            //================================================
            // ALIGNMENT
            //================================================

            sheet.Cells[row, 1]
                .Style.HorizontalAlignment =
                ExcelHorizontalAlignment.Center;

            sheet.Cells[row, 2]
                .Style.HorizontalAlignment =
                ExcelHorizontalAlignment.Left;

            sheet.Cells[row, 2]
                .Style.WrapText =
                true;

            sheet.Cells[row, 3]
                .Style.HorizontalAlignment =
                ExcelHorizontalAlignment.Center;

            for (int column = 4;
                 column <= 8;
                 column++)
            {
                sheet.Cells[row, column]
                    .Style.HorizontalAlignment =
                    ExcelHorizontalAlignment.Right;
            }

            sheet.Row(row).Height = 32;

            row++;
        }


        //================================================
        // TOTAL ROW
        //================================================

        for (int column = 1;
             column <= 8;
             column++)
        {
            StyleTotalCell(
                sheet.Cells[row, column]);
        }

        sheet.Cells[row, 1].Value =
            "TOTALS";

        sheet.Cells[row, 1]
            .Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Center;


        sheet.Cells[row, 4].Value =
            Convert.ToDouble(invoice.TotalQty);

        sheet.Cells[row, 4]
            .Style.Numberformat.Format =
            "#,##0.00";


        sheet.Cells[row, 6].Value =
            Convert.ToDouble(invoice.TotalAmount);

        sheet.Cells[row, 6]
            .Style.Numberformat.Format =
            "₹ #,##0.00";


        sheet.Cells[row, 8].Value =
            Convert.ToDouble(invoice.TotalTax);

        sheet.Cells[row, 8]
            .Style.Numberformat.Format =
            "₹ #,##0.00";


        sheet.Cells[row, 4]
            .Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Right;

        sheet.Cells[row, 6]
            .Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Right;

        sheet.Cells[row, 8]
            .Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Right;

        sheet.Row(row).Height =
            ExcelTheme.TotalRowHeight;

        return row + 2;
    }


    //====================================================
    // GRAND TOTAL
    //====================================================

    private static int AddGrandTotal(
        ExcelWorksheet sheet,
        SalesInvoiceExportDto invoice,
        int row)
    {
        sheet.Cells[row, 1, row, 6].Merge = true;

        sheet.Cells[row, 7, row, 8].Merge = true;

        var label =
            sheet.Cells[row, 1];

        var value =
            sheet.Cells[row, 7];

        label.Value =
            "GRAND TOTAL";

        value.Value =
            Convert.ToDouble(invoice.GrandTotal);

        StyleGrandTotal(label);
        StyleGrandTotal(value);

        value.Style.Numberformat.Format =
            "₹ #,##0.00";

        value.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Right;

        sheet.Row(row).Height = 28;

        return row + 3;
    }


    //====================================================
    // SIGNATURES
    //====================================================

    private static int AddSignatures(
        ExcelWorksheet sheet,
        int row)
    {
        sheet.Cells[row, 1, row, 4].Merge = true;
        sheet.Cells[row, 5, row, 8].Merge = true;

        sheet.Cells[row, 1].Value =
            "____________________________";

        sheet.Cells[row, 5].Value =
            "____________________________";

        sheet.Cells[row, 1]
            .Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Center;

        sheet.Cells[row, 5]
            .Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Center;

        row++;

        sheet.Cells[row, 1, row, 4].Merge = true;
        sheet.Cells[row, 5, row, 8].Merge = true;

        sheet.Cells[row, 1].Value =
            "Customer Signature";

        sheet.Cells[row, 5].Value =
            "Authorized Signature";

        sheet.Cells[row, 1].Style.Font.Bold =
            true;

        sheet.Cells[row, 5].Style.Font.Bold =
            true;

        sheet.Cells[row, 1]
            .Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Center;

        sheet.Cells[row, 5]
            .Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Center;

        return row + 2;
    }


    //====================================================
    // FOOTER
    //====================================================

    private static void AddFooter(
        ExcelWorksheet sheet,
        int row)
    {
        sheet.Cells[row, 1, row, 8].Merge = true;

        sheet.Cells[row, 1]
            .Style.Border.Top.Style =
            ExcelBorderStyle.Medium;

        sheet.Cells[row, 1]
            .Style.Border.Top.Color
            .SetColor(
                SharedExcelStyles.Primary);

        row++;

        sheet.Cells[row, 1, row, 4].Merge = true;
        sheet.Cells[row, 5, row, 8].Merge = true;

        sheet.Cells[row, 1].Value =
            $"Printed On : " +
            $"{DateTime.Now:dd-MM-yyyy HH:mm}";

        sheet.Cells[row, 5].Value =
            ExcelTheme.CompanyName;

        sheet.Cells[row, 1]
            .Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Left;

        sheet.Cells[row, 5]
            .Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Right;

        sheet.Cells[row, 5].Style.Font.Bold =
            true;
    }


    //====================================================
    // SECTION HEADER
    //====================================================

    private static void StyleSectionHeader(
        ExcelRange cell,
        string text)
    {
        cell.Value =
            text;

        cell.Style.Font.Name =
            ExcelTheme.FontName;

        cell.Style.Font.Size =
            ExcelTheme.SectionFontSize;

        cell.Style.Font.Bold =
            true;

        cell.Style.Font.Color
            .SetColor(
                SharedExcelStyles.White);

        cell.Style.Fill.PatternType =
            ExcelFillStyle.Solid;

        cell.Style.Fill.BackgroundColor
            .SetColor(
                SharedExcelStyles.Secondary);

        cell.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Center;

        cell.Style.VerticalAlignment =
            ExcelVerticalAlignment.Center;

        ApplyBorder(cell);
    }


    //====================================================
    // TABLE HEADER
    //====================================================

    private static void StyleTableHeader(
        ExcelRange cell)
    {
        cell.Style.Font.Name =
            ExcelTheme.FontName;

        cell.Style.Font.Size =
            ExcelTheme.HeaderFontSize;

        cell.Style.Font.Bold =
            true;

        cell.Style.Font.Color
            .SetColor(
                SharedExcelStyles.White);

        cell.Style.Fill.PatternType =
            ExcelFillStyle.Solid;

        cell.Style.Fill.BackgroundColor
            .SetColor(
                SharedExcelStyles.HeaderBackground);

        cell.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Center;

        cell.Style.VerticalAlignment =
            ExcelVerticalAlignment.Center;

        cell.Style.WrapText =
            true;

        ApplyBorder(cell);
    }


    //====================================================
    // DATA ROW
    //====================================================

    private static void StyleDataRow(
        ExcelWorksheet sheet,
        int row)
    {
        var range =
            sheet.Cells[row, 1, row, 8];

        range.Style.Font.Name =
            ExcelTheme.FontName;

        range.Style.Font.Size =
            ExcelTheme.BodyFontSize;

        range.Style.Font.Color
            .SetColor(
                SharedExcelStyles.Black);

        range.Style.VerticalAlignment =
            ExcelVerticalAlignment.Center;

        if (row % 2 == 0)
        {
            range.Style.Fill.PatternType =
                ExcelFillStyle.Solid;

            range.Style.Fill.BackgroundColor
                .SetColor(
                    SharedExcelStyles.AlternateRow);
        }

        ApplyBorder(range);
    }


    //====================================================
    // TOTAL CELL
    //====================================================

    private static void StyleTotalCell(
        ExcelRange cell)
    {
        cell.Style.Font.Name =
            ExcelTheme.FontName;

        cell.Style.Font.Size =
            ExcelTheme.BodyFontSize;

        cell.Style.Font.Bold =
            true;

        cell.Style.Font.Color
            .SetColor(
                SharedExcelStyles.Secondary);

        cell.Style.Fill.PatternType =
            ExcelFillStyle.Solid;

        cell.Style.Fill.BackgroundColor
            .SetColor(
                SharedExcelStyles.TotalBackground);

        cell.Style.VerticalAlignment =
            ExcelVerticalAlignment.Center;

        ApplyBorder(cell);
    }


    //====================================================
    // GRAND TOTAL STYLE
    //====================================================

    private static void StyleGrandTotal(
        ExcelRange cell)
    {
        cell.Style.Font.Name =
            ExcelTheme.FontName;

        cell.Style.Font.Size =
            ExcelTheme.SectionFontSize;

        cell.Style.Font.Bold =
            true;

        cell.Style.Font.Color
            .SetColor(
                SharedExcelStyles.White);

        cell.Style.Fill.PatternType =
            ExcelFillStyle.Solid;

        cell.Style.Fill.BackgroundColor
            .SetColor(
                SharedExcelStyles.Secondary);

        cell.Style.VerticalAlignment =
            ExcelVerticalAlignment.Center;

        ApplyBorder(cell);
    }


    //====================================================
    // LABEL + VALUE
    //====================================================

    private static void AddLabelValue(
        ExcelWorksheet sheet,
        int row,
        int column,
        string label,
        string value)
    {
        var labelCell =
            sheet.Cells[row, column];

        labelCell.Value =
            $"{label} :";

        labelCell.Style.Font.Bold =
            true;

        labelCell.Style.Font.Size =
            ExcelTheme.SmallFontSize;

        labelCell.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Left;


        var valueCell =
            sheet.Cells[row, column + 1];

        valueCell.Value =
            value;

        valueCell.Style.Font.Size =
            ExcelTheme.SmallFontSize;

        valueCell.Style.HorizontalAlignment =
            ExcelHorizontalAlignment.Left;
    }


    //====================================================
    // BORDER
    //====================================================

    private static void ApplyBorder(
        ExcelRange range)
    {
        range.Style.Border.Top.Style =
            SharedExcelStyles.Border;

        range.Style.Border.Bottom.Style =
            SharedExcelStyles.Border;

        range.Style.Border.Left.Style =
            SharedExcelStyles.Border;

        range.Style.Border.Right.Style =
            SharedExcelStyles.Border;

        range.Style.Border.Top.Color
            .SetColor(
                SharedExcelStyles.BorderColor);

        range.Style.Border.Bottom.Color
            .SetColor(
                SharedExcelStyles.BorderColor);

        range.Style.Border.Left.Color
            .SetColor(
                SharedExcelStyles.BorderColor);

        range.Style.Border.Right.Color
            .SetColor(
                SharedExcelStyles.BorderColor);
    }


    //====================================================
    // PRINT SETTINGS
    //====================================================

    private static void ConfigurePrintArea(
        ExcelWorksheet sheet,
        int lastRow,
        int itemHeaderRow)
    {
        //------------------------------------------------
        // PRINT AREA
        //------------------------------------------------

        sheet.PrinterSettings.PrintArea =
            sheet.Cells[
                1,
                1,
                lastRow,
                8];


        //------------------------------------------------
        // REPEAT ITEM HEADER
        //
        // EPPlus 8 uses RepeatRows.
        //------------------------------------------------

        sheet.PrinterSettings.RepeatRows =
            new ExcelAddress(
                $"{itemHeaderRow}:{itemHeaderRow}");
    }
}