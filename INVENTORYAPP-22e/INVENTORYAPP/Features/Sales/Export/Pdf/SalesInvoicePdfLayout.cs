

using INVENTORYAPP.Features.Sales.Export.DTOs;
using INVENTORYAPP.Shared.Pdf;

using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;

using iText.Kernel.Pdf.Canvas.Draw;

namespace INVENTORYAPP.Features.Sales.Export.Pdf;

public static class SalesInvoicePdfLayout
{
    //====================================================
    // SALES INVOICE LAYOUT SETTINGS
    //====================================================

    /*
     * IMPORTANT
     * ----------
     * These settings belong ONLY to the Sales Invoice.
     *
     * Shared PDF classes are not modified here because
     * Business Reports and other PDF documents also use
     * the Shared.Pdf infrastructure.
     */

    private const float MinimumFillerHeight = 20f;

    /*
     * This is intentionally smaller than the previous
     * 440f value.
     *
     * The purpose is NOT to force the item table to a
     * fixed height.
     *
     * It provides reasonable empty space for invoices
     * with only a few items while leaving enough room
     * for Grand Total, Signatures and Footer.
     */
    private const float SmallInvoiceGridHeight = 300f;

    private const float EstimatedHeaderHeight = 35f;

    private const float EstimatedTotalsHeight = 35f;

    private const float EstimatedItemBaseHeight = 55f;

    private const float EstimatedDescriptionLineHeight = 11f;


    //====================================================
    // BUILD COMPLETE INVOICE
    //====================================================

    public static void Build(
        Document document,
        SalesInvoiceExportDto invoice)
    {
        //------------------------------------------
        // Company Header
        //------------------------------------------

        AddCompanyHeader(
            document,
            invoice);

        //------------------------------------------
        // Customer + Invoice Details
        //------------------------------------------

        AddInformationSection(
            document,
            invoice);

        //------------------------------------------
        // Item Grid
        //------------------------------------------

        AddItemsTable(
            document,
            invoice);

        //------------------------------------------
        // Final Section
        //
        // Grand Total
        // Signatures
        // Footer
        //------------------------------------------

        AddFinalSection(
            document,
            invoice);
    }


    //====================================================
    // COMPANY HEADER
    //====================================================

    //private static void AddCompanyHeader(
    //    Document document,
    //    SalesInvoiceExportDto invoice)
    //{
    //    //------------------------------------------
    //    // Company Name
    //    //------------------------------------------

    //    document.Add(
    //        PdfHelper.Paragraph(
    //            invoice.CompanyName,
    //            18,
    //            true,
    //            PdfColors.Primary)
    //        .SetTextAlignment(
    //            TextAlignment.CENTER)
    //        .SetMarginBottom(2)
    //    );

    //    //------------------------------------------
    //    // Company Address
    //    //------------------------------------------

    //    if (!string.IsNullOrWhiteSpace(
    //        invoice.CompanyAddress))
    //    {
    //        document.Add(
    //            PdfHelper.Paragraph(
    //                invoice.CompanyAddress,
    //                10)
    //            .SetTextAlignment(
    //                TextAlignment.CENTER)
    //            .SetMarginBottom(2)
    //        );
    //    }

    //    //------------------------------------------
    //    // Phone + Email
    //    //------------------------------------------

    //    var contact =
    //        $"Phone : {invoice.CompanyPhone}    |    " +
    //        $"Email : {invoice.CompanyEmail}";

    //    document.Add(
    //        PdfHelper.Paragraph(
    //            contact,
    //            9)
    //        .SetTextAlignment(
    //            TextAlignment.CENTER)
    //        .SetMarginBottom(2)
    //    );

    //    //------------------------------------------
    //    // GSTIN
    //    //------------------------------------------

    //    if (!string.IsNullOrWhiteSpace(
    //        invoice.CompanyGSTIN))
    //    {
    //        document.Add(
    //            PdfHelper.Paragraph(
    //                $"GSTIN : {invoice.CompanyGSTIN}",
    //                9,
    //                true)
    //            .SetTextAlignment(
    //                TextAlignment.CENTER)
    //            .SetMarginBottom(4)
    //        );
    //    }

    //------------------------------------------
    // Separator
    //------------------------------------------

    //    var separator =
    //        new SolidLine(1f);

    //    separator.SetColor(
    //        PdfColors.Primary);

    //    document.Add(
    //        new LineSeparator(
    //            separator)
    //        .SetMarginTop(2)
    //        .SetMarginBottom(4)
    //    );

    //    //------------------------------------------
    //    // Invoice Title
    //    //------------------------------------------

    //    document.Add(
    //        PdfHelper.Paragraph(
    //            "SALES INVOICE",
    //            17,
    //            true,
    //            PdfColors.Primary)
    //        .SetTextAlignment(
    //            TextAlignment.CENTER)
    //        .SetMarginTop(2)
    //        .SetMarginBottom(6)
    //    );
    //}




    //====================================================
    // COMPANY HEADER
    //====================================================

    private static void AddCompanyHeader(
        Document document,
        SalesInvoiceExportDto invoice)
    {
        //------------------------------------------
        // Company Name
        //------------------------------------------

        document.Add(
            PdfHelper.Paragraph(
                invoice.CompanyName,
                18,
                true,
                PdfColors.Primary)
            .SetTextAlignment(
                TextAlignment.LEFT)
            .SetMarginBottom(2)
        );

        //------------------------------------------
        // Company Address
        //------------------------------------------

        if (!string.IsNullOrWhiteSpace(
            invoice.CompanyAddress))
        {
            document.Add(
                PdfHelper.Paragraph(
                    invoice.CompanyAddress,
                    10)
                .SetTextAlignment(
                    TextAlignment.LEFT)
                .SetMarginBottom(2)
            );
        }

        //------------------------------------------
        // Phone + Email
        //------------------------------------------

        var contact =
            $"Phone : {invoice.CompanyPhone}    |    " +
            $"Email : {invoice.CompanyEmail}";

        document.Add(
            PdfHelper.Paragraph(
                contact,
                9)
            .SetTextAlignment(
                TextAlignment.LEFT)
            .SetMarginBottom(2)
        );

        //------------------------------------------
        // GSTIN
        //------------------------------------------

        if (!string.IsNullOrWhiteSpace(
            invoice.CompanyGSTIN))
        {
            document.Add(
                PdfHelper.Paragraph(
                    $"GSTIN : {invoice.CompanyGSTIN}",
                    9,
                    true)
                .SetTextAlignment(
                    TextAlignment.LEFT)
                .SetMarginBottom(4)
            );
        }

        //------------------------------------------
        // Header Separator
        //------------------------------------------

        var separator =
            new SolidLine(1f);

        separator.SetColor(
            PdfColors.Primary);

        document.Add(
            new LineSeparator(
                separator)
            .SetMarginTop(2)
            .SetMarginBottom(4)
        );

        //------------------------------------------
        // Invoice Title
        //
        // IMPORTANT:
        // Keep this CENTER aligned.
        //------------------------------------------

        document.Add(
            PdfHelper.Paragraph(
                "SALES INVOICE",
                17,
                true,
                PdfColors.Primary)
            .SetTextAlignment(
                TextAlignment.CENTER)
            .SetMarginTop(2)
            .SetMarginBottom(6)
        );
    }











    //====================================================
    // CUSTOMER + INVOICE DETAILS
    //====================================================

    private static void AddInformationSection(
        Document document,
        SalesInvoiceExportDto invoice)
    {
        var table =
            PdfTable.Create(50, 50);

        //------------------------------------------
        // CUSTOMER DETAILS
        //------------------------------------------

        var customerCell =
            new Cell()
                .SetBorder(
                    PdfTheme.TableBorder)
                .SetPadding(8);

        customerCell.Add(
            PdfHelper.Paragraph(
                "CUSTOMER DETAILS",
                10,
                true,
                PdfColors.White)
            .SetBackgroundColor(
                PdfColors.Secondary)
            .SetPadding(4)
        );

        //------------------------------------------
        // Party Name
        //------------------------------------------

        customerCell.Add(
            PdfHelper.Paragraph(
                $"Party Name    :    {invoice.PartyName}",
                9,
                true)
            .SetMarginTop(8)
            .SetMarginBottom(5)
        );

        //------------------------------------------
        // Party Code
        //------------------------------------------

        customerCell.Add(
            PdfHelper.Paragraph(
                $"Party Code    :    {invoice.PartyCode}",
                9)
            .SetMarginBottom(5)
        );

        table.AddCell(
            customerCell);


        //------------------------------------------
        // INVOICE DETAILS
        //------------------------------------------

        var invoiceCell =
            new Cell()
                .SetBorder(
                    PdfTheme.TableBorder)
                .SetPadding(8);

        invoiceCell.Add(
            PdfHelper.Paragraph(
                "INVOICE DETAILS",
                10,
                true,
                PdfColors.White)
            .SetBackgroundColor(
                PdfColors.Secondary)
            .SetPadding(4)
        );

        //------------------------------------------
        // Invoice No
        //------------------------------------------

        invoiceCell.Add(
            PdfHelper.Paragraph(
                $"Invoice No    :    {invoice.InvoiceNo}",
                9,
                true)
            .SetMarginTop(8)
            .SetMarginBottom(4)
        );

        //------------------------------------------
        // Invoice Date
        //------------------------------------------

        invoiceCell.Add(
            PdfHelper.Paragraph(
                $"Invoice Date  :    {invoice.InvoiceDate:dd-MM-yyyy}",
                9)
            .SetMarginBottom(4)
        );

        //------------------------------------------
        // Generated By
        //------------------------------------------

        invoiceCell.Add(
            PdfHelper.Paragraph(
                $"Generated By  :    {invoice.GeneratedBy}",
                9)
            .SetMarginBottom(4)
        );

        //------------------------------------------
        // Generated On
        //------------------------------------------

        invoiceCell.Add(
            PdfHelper.Paragraph(
                $"Generated On  :    {invoice.GeneratedOn:dd-MM-yyyy hh:mm tt}",
                9)
            .SetMarginBottom(4)
        );

        table.AddCell(
            invoiceCell);

        //------------------------------------------
        // Add Information Table
        //------------------------------------------

        document.Add(table);

        //------------------------------------------
        // Small Gap
        //------------------------------------------

        document.Add(
            new Paragraph(" ")
                .SetMarginTop(1)
                .SetMarginBottom(1)
        );
    }


    //====================================================
    // ITEM GRID
    //====================================================

    private static void AddItemsTable(
        Document document,
        SalesInvoiceExportDto invoice)
    {
        var table =
            new Table(
                UnitValue.CreatePercentArray(
                    new float[]
                    {
                        6,      // SL
                        34,     // ITEM DETAILS
                        9,      // UNIT
                        8,      // QTY
                        11,     // RATE
                        12,     // AMOUNT
                        8,      // TAX %
                        12      // TAX AMOUNT
                    }))
            .UseAllAvailableWidth();

        //------------------------------------------
        // Header
        //------------------------------------------

        AddItemHeader(
            table,
            "SL",
            "ITEM DETAILS",
            "UNIT",
            "QTY",
            "RATE",
            "AMOUNT",
            "TAX %",
            "TAX AMOUNT");

        //------------------------------------------
        // Item Rows
        //------------------------------------------

        bool alternate = false;

        foreach (var item in invoice.Items)
        {
            AddItemRow(
                table,
                item,
                alternate);

            alternate = !alternate;
        }

        //------------------------------------------
        // Filler
        //
        // Filler exists only when the invoice is
        // small enough to benefit from empty grid
        // space.
        //------------------------------------------

        AddFillerRow(
            table,
            invoice);

        //------------------------------------------
        // Totals
        //------------------------------------------

        AddGridTotals(
            table,
            invoice);

        //------------------------------------------
        // Add Table
        //------------------------------------------

        document.Add(table);
    }


    //====================================================
    // ITEM HEADER
    //====================================================

    private static void AddItemHeader(
        Table table,
        params string[] headers)
    {
        foreach (var header in headers)
        {
            table.AddHeaderCell(
                PdfHelper.Cell(
                    header,
                    true,
                    PdfColors.Header,
                    TextAlignment.CENTER)
                .SetFontColor(
                    PdfColors.White)
                .SetPadding(6)
            );
        }
    }


    //====================================================
    // ITEM ROW
    //====================================================

    private static void AddItemRow(
        Table table,
        SalesInvoiceItemExportDto item,
        bool alternate)
    {
        var background =
            alternate
                ? PdfColors.AlternateRow
                : null;

        //------------------------------------------
        // SL
        //------------------------------------------

        table.AddCell(
            PdfHelper.Cell(
                item.SlNo.ToString(),
                false,
                background,
                TextAlignment.CENTER)
        );

        //------------------------------------------
        // ITEM DETAILS
        //
        // Stock Code + Stock Name
        // Description below
        //------------------------------------------

        var itemCell =
            new Cell()
                .SetBorder(
                    PdfTheme.TableBorder)
                .SetPadding(8);

        //------------------------------------------
        // Stock Code + Stock Name
        //------------------------------------------

        itemCell.Add(
            PdfHelper.Paragraph(
                $"{item.StockCode} - {item.StockName}",
                9,
                true)
            .SetMarginBottom(4)
        );

        //------------------------------------------
        // Description
        //------------------------------------------

        if (!string.IsNullOrWhiteSpace(
            item.Description))
        {
            itemCell.Add(
                PdfHelper.Paragraph(
                    item.Description,
                    8)
            );
        }

        //------------------------------------------
        // Background
        //------------------------------------------

        if (background != null)
        {
            itemCell.SetBackgroundColor(
                background);
        }

        table.AddCell(
            itemCell);

        //------------------------------------------
        // UNIT
        //------------------------------------------

        table.AddCell(
            PdfHelper.Cell(
                item.Unit,
                false,
                background,
                TextAlignment.CENTER)
        );

        //------------------------------------------
        // QTY
        //------------------------------------------

        table.AddCell(
            PdfHelper.NumberCell(
                item.Qty,
                false,
                background)
        );

        //------------------------------------------
        // RATE
        //------------------------------------------

        table.AddCell(
            PdfHelper.NumberCell(
                item.Rate,
                false,
                background)
        );

        //------------------------------------------
        // AMOUNT
        //------------------------------------------

        table.AddCell(
            PdfHelper.NumberCell(
                item.Amount,
                false,
                background)
        );

        //------------------------------------------
        // TAX %
        //------------------------------------------

        table.AddCell(
            PdfHelper.NumberCell(
                item.TaxRate,
                false,
                background)
        );

        //------------------------------------------
        // TAX AMOUNT
        //------------------------------------------

        table.AddCell(
            PdfHelper.NumberCell(
                item.TaxAmount,
                false,
                background)
        );
    }


    //====================================================
    // DYNAMIC FILLER ROW
    //
    // IMPORTANT:
    //
    // The filler belongs INSIDE the item grid.
    //
    // It is used only for small invoices.
    //
    // Large invoices naturally consume the available
    // page and are allowed to continue onto another
    // page when necessary.
    //====================================================

    private static void AddFillerRow(
        Table table,
        SalesInvoiceExportDto invoice)
    {
        //------------------------------------------
        // Only use filler for small invoices
        //------------------------------------------

        if (invoice.Items.Count > 3)
        {
            return;
        }

        //------------------------------------------
        // Estimate item height
        //------------------------------------------

        var estimatedItemHeight =
            0f;

        foreach (var item in invoice.Items)
        {
            estimatedItemHeight +=
                EstimateItemHeight(item);
        }

        //------------------------------------------
        // Estimate current grid height
        //------------------------------------------

        var estimatedUsedHeight =
            EstimatedHeaderHeight
            + estimatedItemHeight
            + EstimatedTotalsHeight;

        //------------------------------------------
        // Calculate filler
        //------------------------------------------

        var fillerHeight =
            SmallInvoiceGridHeight
            - estimatedUsedHeight;

        //------------------------------------------
        // Do not create tiny filler
        //------------------------------------------

        if (fillerHeight <
            MinimumFillerHeight)
        {
            return;
        }

        //------------------------------------------
        // One complete row spanning all columns
        //------------------------------------------

        var fillerCell =
            new Cell(
                1,
                8)
            .SetBorder(
                PdfTheme.TableBorder)
            .SetMinHeight(
                fillerHeight);

        table.AddCell(
            fillerCell);
    }


    //====================================================
    // ESTIMATE ITEM HEIGHT
    //====================================================

    private static float EstimateItemHeight(
        SalesInvoiceItemExportDto item)
    {
        //------------------------------------------
        // Base
        //------------------------------------------

        var height =
            EstimatedItemBaseHeight;

        //------------------------------------------
        // Description
        //------------------------------------------

        if (!string.IsNullOrWhiteSpace(
            item.Description))
        {
            const int charactersPerLine =
                55;

            var lines =
                (int)Math.Ceiling(
                    item.Description.Length /
                    (double)charactersPerLine);

            if (lines < 1)
            {
                lines = 1;
            }

            height +=
                (lines - 1)
                * EstimatedDescriptionLineHeight;
        }

        //------------------------------------------
        // Minimum
        //------------------------------------------

        return Math.Max(
            height,
            40f);
    }


    //====================================================
    // GRID TOTALS
    //====================================================

    private static void AddGridTotals(
        Table table,
        SalesInvoiceExportDto invoice)
    {
        //------------------------------------------
        // TOTAL LABEL
        //------------------------------------------

        table.AddCell(
            PdfHelper.Cell(
                "TOTALS",
                true,
                PdfColors.TotalRow,
                TextAlignment.CENTER)
        );

        //------------------------------------------
        // ITEM DETAILS
        //------------------------------------------

        table.AddCell(
            PdfHelper.Cell(
                string.Empty,
                true,
                PdfColors.TotalRow)
        );

        //------------------------------------------
        // UNIT
        //------------------------------------------

        table.AddCell(
            PdfHelper.Cell(
                string.Empty,
                true,
                PdfColors.TotalRow)
        );

        //------------------------------------------
        // QTY
        //------------------------------------------

        table.AddCell(
            PdfHelper.NumberCell(
                invoice.TotalQty,
                true,
                PdfColors.TotalRow)
        );

        //------------------------------------------
        // RATE
        //------------------------------------------

        table.AddCell(
            PdfHelper.Cell(
                string.Empty,
                true,
                PdfColors.TotalRow)
        );

        //------------------------------------------
        // AMOUNT
        //------------------------------------------

        table.AddCell(
            PdfHelper.NumberCell(
                invoice.TotalAmount,
                true,
                PdfColors.TotalRow)
        );

        //------------------------------------------
        // TAX %
        //------------------------------------------

        table.AddCell(
            PdfHelper.Cell(
                string.Empty,
                true,
                PdfColors.TotalRow)
        );

        //------------------------------------------
        // TAX
        //------------------------------------------

        table.AddCell(
            PdfHelper.NumberCell(
                invoice.TotalTax,
                true,
                PdfColors.TotalRow)
        );
    }


    //====================================================
    // FINAL SECTION
    //
    // Grand Total
    // Signature
    // Footer
    //
    // These are kept together as the final invoice
    // section whenever iText has enough room.
    //====================================================

    private static void AddFinalSection(
        Document document,
        SalesInvoiceExportDto invoice)
    {
        //------------------------------------------
        // Final Section Container
        //------------------------------------------

        var finalSection =
            new Div()
                .SetKeepTogether(
                    true);

        //------------------------------------------
        // Grand Total
        //------------------------------------------

        AddGrandTotal(
            finalSection,
            invoice);

        //------------------------------------------
        // Signature
        //------------------------------------------

        AddSignatureArea(
            finalSection);

        //------------------------------------------
        // Footer
        //------------------------------------------

        AddFooter(
            finalSection);

        //------------------------------------------
        // Add Final Section
        //------------------------------------------

        document.Add(
            finalSection);
    }


    //====================================================
    // GRAND TOTAL
    //====================================================

    private static void AddGrandTotal(
        Div container,
        SalesInvoiceExportDto invoice)
    {
        var table =
            PdfTable.Create(
                70,
                30);

        //------------------------------------------
        // Label
        //------------------------------------------

        table.AddCell(
            PdfHelper.Cell(
                "GRAND TOTAL",
                true,
                PdfColors.Secondary,
                TextAlignment.LEFT)
            .SetFontColor(
                PdfColors.White)
            .SetPadding(8)
        );

        //------------------------------------------
        // Amount
        //------------------------------------------

        table.AddCell(
            PdfHelper.NumberCell(
                invoice.GrandTotal,
                true,
                PdfColors.Secondary)
            .SetFontColor(
                PdfColors.White)
            .SetPadding(8)
        );

        //------------------------------------------
        // Add
        //------------------------------------------

        container.Add(
            table);
    }


    //====================================================
    // SIGNATURE AREA
    //====================================================

    private static void AddSignatureArea(
        Div container)
    {
        //------------------------------------------
        // Signature Space
        //------------------------------------------

        container.Add(
            new Paragraph(" ")
                .SetMinHeight(45)
                .SetMarginTop(0)
                .SetMarginBottom(0)
        );

        //------------------------------------------
        // Signature Table
        //------------------------------------------

        var table =
            PdfTable.Create(
                50,
                50);

        //------------------------------------------
        // Customer
        //------------------------------------------

        table.AddCell(
            PdfHelper.Cell(
                "____________________________\n" +
                "Customer Signature",
                true,
                null,
                TextAlignment.CENTER)
            .SetBorder(
                Border.NO_BORDER)
        );

        //------------------------------------------
        // Authorized
        //------------------------------------------

        table.AddCell(
            PdfHelper.Cell(
                "____________________________\n" +
                "Authorized Signature",
                true,
                null,
                TextAlignment.CENTER)
            .SetBorder(
                Border.NO_BORDER)
        );

        //------------------------------------------
        // Add
        //------------------------------------------

        container.Add(
            table);
    }


    //====================================================
    // FOOTER
    //====================================================

    private static void AddFooter(
        Div container)
    {
        //------------------------------------------
        // Footer Separator
        //------------------------------------------

        var footerSeparator =
            new SolidLine(0.8f);

        footerSeparator.SetColor(
            PdfColors.Primary);

        container.Add(
            new LineSeparator(
                footerSeparator)
            .SetMarginTop(8)
            .SetMarginBottom(4)
        );

        //------------------------------------------
        // Footer Table
        //------------------------------------------

        var footer =
            PdfTable.Create(
                70,
                30);

        //------------------------------------------
        // Printed On
        //------------------------------------------

        footer.AddCell(
            PdfHelper.Cell(
                $"Printed On : {DateTime.Now:dd-MM-yyyy hh:mm tt}",
                false)
            .SetBorder(
                Border.NO_BORDER)
        );

        //------------------------------------------
        // Right Side
        //------------------------------------------

        footer.AddCell(
            PdfHelper.Cell(
                "INVENTORY ERP",
                true,
                null,
                TextAlignment.RIGHT)
            .SetBorder(
                Border.NO_BORDER)
        );

        //------------------------------------------
        // Add Footer
        //------------------------------------------

        container.Add(
            footer);
    }
}