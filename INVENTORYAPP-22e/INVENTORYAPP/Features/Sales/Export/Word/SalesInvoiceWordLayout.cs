using INVENTORYAPP.Features.Sales.Export.DTOs;
using INVENTORYAPP.Shared.Word;

using Xceed.Document.NET;
using Xceed.Words.NET;

namespace INVENTORYAPP.Features.Sales.Export.Word;

public static class SalesInvoiceWordLayout
{
    //====================================================
    // A4 PAGE
    //====================================================

    private const float A4Width = 595.28f;
    private const float A4Height = 841.89f;

    private const float MarginLeft = 28f;
    private const float MarginRight = 28f;
    private const float MarginTop = 24f;
    private const float MarginBottom = 24f;


    //====================================================
    // BUILD
    //====================================================

    public static void Build(
        DocX document,
        SalesInvoiceExportDto invoice)
    {
        ConfigurePage(document);

        AddCompanyHeader(
            document,
            invoice);

        AddInvoiceInformation(
            document,
            invoice);

        AddItemsGrid(
            document,
            invoice);

        AddGrandTotal(
            document,
            invoice);

        AddSignatures(
            document);

        AddFooter(
            document);
    }


    //====================================================
    // PAGE CONFIGURATION
    //====================================================

    private static void ConfigurePage(
        DocX document)
    {
        document.PageWidth =
            A4Width;

        document.PageHeight =
            A4Height;

        document.MarginLeft =
            MarginLeft;

        document.MarginRight =
            MarginRight;

        document.MarginTop =
            MarginTop;

        document.MarginBottom =
            MarginBottom;
    }


    //====================================================
    // COMPANY HEADER
    //
    // MATCHES PDF:
    //
    // Company Name -> Primary
    // Address      -> Black
    // Contact      -> Black
    // GSTIN        -> Black
    // Separator    -> Primary
    // Invoice      -> Primary
    //====================================================

    private static void AddCompanyHeader(
        DocX document,
        SalesInvoiceExportDto invoice)
    {
        //------------------------------------------------
        // COMPANY NAME
        //------------------------------------------------

        var company =
            document.InsertParagraph();

        company.Append(
                invoice.CompanyName)
            .Bold()
            .FontSize(18f)
            .Color(WordColors.Primary);

        company.Alignment =
            Alignment.left;

        company.SpacingAfter(
            2d);


        //------------------------------------------------
        // ADDRESS
        //------------------------------------------------

        if (!string.IsNullOrWhiteSpace(
            invoice.CompanyAddress))
        {
            var address =
                document.InsertParagraph();

            address.Append(
                    invoice.CompanyAddress)
                .FontSize(10f)
                .Color(WordColors.Black);

            address.Alignment =
                Alignment.left;

            address.SpacingAfter(
                2d);
        }


        //------------------------------------------------
        // PHONE + EMAIL
        //------------------------------------------------

        var contact =
            document.InsertParagraph();

        contact.Append(
                $"Phone : {invoice.CompanyPhone}    |    " +
                $"Email : {invoice.CompanyEmail}")
            .FontSize(9f)
            .Color(WordColors.Black);

        contact.Alignment =
            Alignment.left;

        contact.SpacingAfter(
            2d);


        //------------------------------------------------
        // GSTIN
        //------------------------------------------------

        if (!string.IsNullOrWhiteSpace(
            invoice.CompanyGSTIN))
        {
            var gst =
                document.InsertParagraph();

            gst.Append(
                    $"GSTIN : {invoice.CompanyGSTIN}")
                .Bold()
                .FontSize(9f)
                .Color(WordColors.Black);

            gst.Alignment =
                Alignment.left;

            gst.SpacingAfter(
                4d);
        }


        //------------------------------------------------
        // SEPARATOR
        //
        // Same role as PDF:
        // PdfColors.Primary
        //------------------------------------------------

        var separator =
            document.InsertParagraph();

        separator.Append(
                "____________________________________________________________")
            .FontSize(6f)
            .Color(WordColors.Primary);

        separator.Alignment =
            Alignment.left;

        separator.SpacingAfter(
            5d);


        //------------------------------------------------
        // SALES INVOICE
        //
        // Center aligned exactly like PDF
        //------------------------------------------------

        var title =
            document.InsertParagraph();

        title.Append(
                "SALES INVOICE")
            .Bold()
            .FontSize(17f)
            .Color(WordColors.Primary);

        title.Alignment =
            Alignment.center;

        title.SpacingAfter(
            8d);
    }


    //====================================================
    // CUSTOMER + INVOICE INFORMATION
    //
    // PDF:
    // PdfColors.Secondary
    //
    // WORD:
    // WordColors.Secondary
    //====================================================

    private static void AddInvoiceInformation(
        DocX document,
        SalesInvoiceExportDto invoice)
    {
        var table =
            document.InsertTable(
                2,
                2);

        table.Design =
            TableDesign.TableGrid;

        table.Alignment =
            Alignment.center;

        table.SetWidths(
            new float[]
            {
                269f,
                269f
            });


        //------------------------------------------------
        // CUSTOMER DETAILS
        //
        // IMPORTANT:
        // DARK BLUE - Secondary
        //------------------------------------------------

        SetSectionHeaderCell(
            table.Rows[0].Cells[0],
            "CUSTOMER DETAILS");


        //------------------------------------------------
        // INVOICE DETAILS
        //
        // IMPORTANT:
        // DARK BLUE - Secondary
        //------------------------------------------------

        SetSectionHeaderCell(
            table.Rows[0].Cells[1],
            "INVOICE DETAILS");


        //------------------------------------------------
        // CUSTOMER INFORMATION
        //------------------------------------------------

        var customer =
            table.Rows[1].Cells[0];

        customer.VerticalAlignment =
            VerticalAlignment.Center;

        SetFirstParagraph(
            customer,
            $"Party Name    :    {invoice.PartyName}",
            true);

        AddCellParagraph(
            customer,
            $"Party Code    :    {invoice.PartyCode}");


        //------------------------------------------------
        // INVOICE INFORMATION
        //------------------------------------------------

        var invoiceCell =
            table.Rows[1].Cells[1];

        invoiceCell.VerticalAlignment =
            VerticalAlignment.Center;

        SetFirstParagraph(
            invoiceCell,
            $"Invoice No    :    {invoice.InvoiceNo}",
            true);

        AddCellParagraph(
            invoiceCell,
            $"Invoice Date  :    " +
            $"{invoice.InvoiceDate:dd-MM-yyyy}");

        AddCellParagraph(
            invoiceCell,
            $"Generated By  :    " +
            $"{invoice.GeneratedBy}");

        AddCellParagraph(
            invoiceCell,
            $"Generated On  :    " +
            $"{invoice.GeneratedOn:dd-MM-yyyy hh:mm tt}");


        //------------------------------------------------
        // SPACE
        //------------------------------------------------

        var space =
            document.InsertParagraph();

        space.SpacingAfter(
            5d);
    }


    //====================================================
    // ITEMS GRID
    //
    // PDF:
    // Header -> Header color
    // Alternate -> AlternateRow
    // Filler -> white
    // Total -> TotalRow
    //
    // Word uses exactly the same color roles.
    //====================================================

    private static void AddItemsGrid(
        DocX document,
        SalesInvoiceExportDto invoice)
    {
        //------------------------------------------------
        // ITEMS
        //------------------------------------------------

        var items =
            invoice.Items ??
            new List<SalesInvoiceItemExportDto>();


        //------------------------------------------------
        // GAP
        //
        // One large empty row only.
        // No unnecessary horizontal lines.
        //------------------------------------------------

        bool addGap =
            items.Count < 6;


        //------------------------------------------------
        // ROW COUNT
        //------------------------------------------------

        int totalRows =
            1 +
            items.Count +
            (addGap ? 1 : 0) +
            1;


        //------------------------------------------------
        // CREATE TABLE
        //------------------------------------------------

        var table =
            document.InsertTable(
                totalRows,
                8);

        table.Design =
            TableDesign.TableGrid;

        table.Alignment =
            Alignment.center;


        //------------------------------------------------
        // COLUMN WIDTHS
        //------------------------------------------------

        table.SetWidths(
            new float[]
            {
                30f,
                165f,
                55f,
                48f,
                60f,
                70f,
                48f,
                63f
            });


        //================================================
        // ITEM TABLE HEADER
        //
        // LIGHT BLUE - Header
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


        for (int column = 0;
             column < headers.Length;
             column++)
        {
            SetItemHeaderCell(
                table.Rows[0].Cells[column],
                headers[column]);
        }


        table.Rows[0].MinHeight =
            30f;


        //================================================
        // ITEM ROWS
        //================================================

        for (int i = 0;
             i < items.Count;
             i++)
        {
            var item =
                items[i];

            var row =
                table.Rows[
                    i + 1];


            row.MinHeight =
                48f;


            //------------------------------------------------
            // ALTERNATE ROW
            //------------------------------------------------

            if (i % 2 == 1)
            {
                for (int c = 0;
                     c < 8;
                     c++)
                {
                    row.Cells[c].FillColor =
                        WordColors.AlternateRow;
                }
            }


            //------------------------------------------------
            // SL
            //------------------------------------------------

            SetCellText(
                row.Cells[0],
                item.SlNo.ToString(),
                Alignment.center);


            //------------------------------------------------
            // ITEM DETAILS
            //------------------------------------------------

            SetItemDetails(
                row.Cells[1],
                item);


            //------------------------------------------------
            // UNIT
            //------------------------------------------------

            SetCellText(
                row.Cells[2],
                item.Unit,
                Alignment.center);


            //------------------------------------------------
            // QTY
            //------------------------------------------------

            SetCellText(
                row.Cells[3],
                item.Qty.ToString("N2"),
                Alignment.right);


            //------------------------------------------------
            // RATE
            //------------------------------------------------

            SetCellText(
                row.Cells[4],
                item.Rate.ToString("N2"),
                Alignment.right);


            //------------------------------------------------
            // AMOUNT
            //------------------------------------------------

            SetCellText(
                row.Cells[5],
                item.Amount.ToString("N2"),
                Alignment.right);


            //------------------------------------------------
            // TAX %
            //------------------------------------------------

            SetCellText(
                row.Cells[6],
                item.TaxRate.ToString("N2"),
                Alignment.right);


            //------------------------------------------------
            // TAX AMOUNT
            //------------------------------------------------

            SetCellText(
                row.Cells[7],
                item.TaxAmount.ToString("N2"),
                Alignment.right);
        }


        //================================================
        // ONE EMPTY GAP ROW
        //================================================

        int gapIndex =
            1 +
            items.Count;


        if (addGap)
        {
            var gapRow =
                table.Rows[
                    gapIndex];


            gapRow.MinHeight =
                GetGapHeight(
                    items.Count);


            //------------------------------------------------
            // WHITE EMPTY AREA
            //------------------------------------------------

            for (int c = 0;
                 c < 8;
                 c++)
            {
                gapRow.Cells[c].FillColor =
                    WordColors.White;

                SetCellText(
                    gapRow.Cells[c],
                    string.Empty,
                    Alignment.left);
            }
        }


        //================================================
        // TOTAL ROW
        //
        // PDF:
        // PdfColors.TotalRow
        //
        // WORD:
        // WordColors.TotalRow
        //================================================

        int totalIndex =
            totalRows - 1;

        var totalRow =
            table.Rows[
                totalIndex];

        totalRow.MinHeight =
            32f;


        //------------------------------------------------
        // TOTAL BACKGROUND
        //------------------------------------------------

        for (int c = 0;
             c < 8;
             c++)
        {
            totalRow.Cells[c].FillColor =
                WordColors.TotalRow;
        }


        //------------------------------------------------
        // TOTAL LABEL
        //------------------------------------------------

        SetTotalCell(
            totalRow.Cells[0],
            "TOTALS",
            Alignment.center);


        //------------------------------------------------
        // ITEM DETAILS
        //------------------------------------------------

        SetTotalCell(
            totalRow.Cells[1],
            string.Empty,
            Alignment.center);


        //------------------------------------------------
        // UNIT
        //------------------------------------------------

        SetTotalCell(
            totalRow.Cells[2],
            string.Empty,
            Alignment.center);


        //------------------------------------------------
        // TOTAL QTY
        //------------------------------------------------

        SetTotalCell(
            totalRow.Cells[3],
            invoice.TotalQty.ToString("N2"),
            Alignment.right);


        //------------------------------------------------
        // RATE
        //------------------------------------------------

        SetTotalCell(
            totalRow.Cells[4],
            string.Empty,
            Alignment.right);


        //------------------------------------------------
        // TOTAL AMOUNT
        //------------------------------------------------

        SetTotalCell(
            totalRow.Cells[5],
            $"₹ {invoice.TotalAmount:N2}",
            Alignment.right);


        //------------------------------------------------
        // TAX %
        //------------------------------------------------

        SetTotalCell(
            totalRow.Cells[6],
            string.Empty,
            Alignment.right);


        //------------------------------------------------
        // TOTAL TAX
        //------------------------------------------------

        SetTotalCell(
            totalRow.Cells[7],
            $"₹ {invoice.TotalTax:N2}",
            Alignment.right);


        //------------------------------------------------
        // SPACE
        //------------------------------------------------

        var space =
            document.InsertParagraph();

        space.SpacingAfter(
            5d);
    }


    //====================================================
    // GAP HEIGHT
    //====================================================

    private static float GetGapHeight(
        int itemCount)
    {
        return itemCount switch
        {
            0 => 300f,
            1 => 250f,
            2 => 205f,
            3 => 160f,
            4 => 115f,
            5 => 70f,
            _ => 0f
        };
    }


    //====================================================
    // ITEM DETAILS
    //====================================================

    private static void SetItemDetails(
        Cell cell,
        SalesInvoiceItemExportDto item)
    {
        cell.VerticalAlignment =
            VerticalAlignment.Center;


        //------------------------------------------------
        // STOCK CODE + STOCK NAME
        //------------------------------------------------

        var first =
            cell.Paragraphs[0];

        first.Append(
                $"{item.StockCode} - {item.StockName}")
            .Bold()
            .FontSize(8.5f)
            .Color(WordColors.Black);

        first.Alignment =
            Alignment.left;

        first.SpacingAfter(
            2d);


        //------------------------------------------------
        // DESCRIPTION
        //------------------------------------------------

        if (!string.IsNullOrWhiteSpace(
            item.Description))
        {
            var description =
                cell.InsertParagraph();

            description.Append(
                    item.Description)
                .FontSize(8f)
                .Color(WordColors.Black);

            description.Alignment =
                Alignment.left;

            description.SpacingAfter(
                0d);
        }
    }


    //====================================================
    // GRAND TOTAL
    //
    // PDF:
    // PdfColors.Secondary
    //
    // WORD:
    // WordColors.Secondary
    //====================================================

    private static void AddGrandTotal(
        DocX document,
        SalesInvoiceExportDto invoice)
    {
        var table =
            document.InsertTable(
                1,
                2);

        table.Design =
            TableDesign.TableGrid;

        table.Alignment =
            Alignment.center;

        table.SetWidths(
            new float[]
            {
                440f,
                99f
            });


        //------------------------------------------------
        // LABEL
        //------------------------------------------------

        var label =
            table.Rows[0].Cells[0];

        label.FillColor =
            WordColors.Secondary;

        label.VerticalAlignment =
            VerticalAlignment.Center;

        var labelParagraph =
            label.Paragraphs[0];

        labelParagraph
            .Append(
                "GRAND TOTAL")
            .Bold()
            .FontSize(11f)
            .Color(WordColors.White);

        labelParagraph.Alignment =
            Alignment.left;


        //------------------------------------------------
        // VALUE
        //------------------------------------------------

        var value =
            table.Rows[0].Cells[1];

        value.FillColor =
            WordColors.Secondary;

        value.VerticalAlignment =
            VerticalAlignment.Center;

        var valueParagraph =
            value.Paragraphs[0];

        valueParagraph
            .Append(
                $"₹ {invoice.GrandTotal:N2}")
            .Bold()
            .FontSize(11f)
            .Color(WordColors.White);

        valueParagraph.Alignment =
            Alignment.right;


        //------------------------------------------------
        // HEIGHT
        //------------------------------------------------

        table.Rows[0].MinHeight =
            36f;


        //------------------------------------------------
        // SPACE
        //------------------------------------------------

        var space =
            document.InsertParagraph();

        space.SpacingAfter(
            15d);
    }


    //====================================================
    // SIGNATURES
    //====================================================

    private static void AddSignatures(
        DocX document)
    {
        var table =
            document.InsertTable(
                1,
                2);

        table.Design =
            TableDesign.TableGrid;

        table.Alignment =
            Alignment.center;

        table.SetWidths(
            new float[]
            {
                269f,
                269f
            });


        //------------------------------------------------
        // CUSTOMER
        //------------------------------------------------

        var customer =
            table.Rows[0].Cells[0];

        customer.VerticalAlignment =
            VerticalAlignment.Center;

        var customerLine =
            customer.Paragraphs[0];

        customerLine
            .Append(
                "____________________________")
            .FontSize(9f)
            .Color(WordColors.Black);

        customerLine.Alignment =
            Alignment.center;

        customerLine.SpacingAfter(
            3d);


        var customerLabel =
            customer.InsertParagraph();

        customerLabel
            .Append(
                "Customer Signature")
            .Bold()
            .FontSize(9f)
            .Color(WordColors.Black);

        customerLabel.Alignment =
            Alignment.center;


        //------------------------------------------------
        // AUTHORIZED
        //------------------------------------------------

        var authorized =
            table.Rows[0].Cells[1];

        authorized.VerticalAlignment =
            VerticalAlignment.Center;

        var authorizedLine =
            authorized.Paragraphs[0];

        authorizedLine
            .Append(
                "____________________________")
            .FontSize(9f)
            .Color(WordColors.Black);

        authorizedLine.Alignment =
            Alignment.center;

        authorizedLine.SpacingAfter(
            3d);


        var authorizedLabel =
            authorized.InsertParagraph();

        authorizedLabel
            .Append(
                "Authorized Signature")
            .Bold()
            .FontSize(9f)
            .Color(WordColors.Black);

        authorizedLabel.Alignment =
            Alignment.center;


        //------------------------------------------------
        // HEIGHT
        //------------------------------------------------

        table.Rows[0].MinHeight =
            65f;
    }


    //====================================================
    // FOOTER
    //====================================================

    private static void AddFooter(
        DocX document)
    {
        //------------------------------------------------
        // SPACE
        //------------------------------------------------

        var space =
            document.InsertParagraph();

        space.SpacingAfter(
            4d);


        //------------------------------------------------
        // SEPARATOR
        //------------------------------------------------

        var separator =
            document.InsertParagraph();

        separator.Append(
                "____________________________________________________________")
            .FontSize(6f)
            .Color(WordColors.Primary);

        separator.Alignment =
            Alignment.left;

        separator.SpacingAfter(
            3d);


        //------------------------------------------------
        // FOOTER
        //------------------------------------------------

        var footer =
            document.InsertParagraph();

        footer.Append(
                $"Printed On : " +
                $"{DateTime.Now:dd-MM-yyyy hh:mm tt}")
            .FontSize(8f)
            .Color(WordColors.Black);

        footer.Alignment =
            Alignment.left;


        footer.Append(
                "                                      INVENTORY ERP")
            .Bold()
            .FontSize(8f)
            .Color(WordColors.Black);
    }


    //====================================================
    // SECTION HEADER CELL
    //
    // USED FOR:
    //
    // CUSTOMER DETAILS
    // INVOICE DETAILS
    //
    // PDF equivalent:
    // PdfColors.Secondary
    //
    // RGB:
    // 13, 71, 161
    //====================================================

    private static void SetSectionHeaderCell(
        Cell cell,
        string text)
    {
        cell.FillColor =
            WordColors.Secondary;

        cell.VerticalAlignment =
            VerticalAlignment.Center;

        var paragraph =
            cell.Paragraphs[0];

        paragraph.Append(
                text)
            .Bold()
            .FontSize(10f)
            .Color(WordColors.White);

        paragraph.Alignment =
            Alignment.center;

        paragraph.SpacingAfter(
            0d);
    }


    //====================================================
    // ITEM TABLE HEADER CELL
    //
    // USED FOR:
    //
    // SL
    // ITEM DETAILS
    // UNIT
    // QTY
    // RATE
    // AMOUNT
    // TAX %
    // TAX AMOUNT
    //
    // PDF equivalent:
    // PdfColors.Header
    //
    // RGB:
    // 33, 150, 243
    //====================================================

    private static void SetItemHeaderCell(
        Cell cell,
        string text)
    {
        cell.FillColor =
            WordColors.Header;

        cell.VerticalAlignment =
            VerticalAlignment.Center;

        var paragraph =
            cell.Paragraphs[0];

        paragraph.Append(
                text)
            .Bold()
            .FontSize(8.5f)
            .Color(WordColors.White);

        paragraph.Alignment =
            Alignment.center;

        paragraph.SpacingAfter(
            0d);
    }


    //====================================================
    // NORMAL CELL
    //====================================================

    private static void SetCellText(
        Cell cell,
        string text,
        Alignment alignment)
    {
        cell.VerticalAlignment =
            VerticalAlignment.Center;

        var paragraph =
            cell.Paragraphs[0];

        paragraph.Append(
                text)
            .FontSize(8.5f)
            .Color(WordColors.Black);

        paragraph.Alignment =
            alignment;

        paragraph.SpacingAfter(
            0d);
    }


    //====================================================
    // TOTAL CELL
    //====================================================

    private static void SetTotalCell(
        Cell cell,
        string text,
        Alignment alignment)
    {
        cell.VerticalAlignment =
            VerticalAlignment.Center;

        var paragraph =
            cell.Paragraphs[0];

        paragraph.Append(
                text)
            .Bold()
            .FontSize(8.5f)
            .Color(WordColors.Secondary);

        paragraph.Alignment =
            alignment;

        paragraph.SpacingAfter(
            0d);
    }


    //====================================================
    // FIRST INFORMATION PARAGRAPH
    //====================================================

    private static void SetFirstParagraph(
        Cell cell,
        string text,
        bool bold)
    {
        var paragraph =
            cell.Paragraphs[0];

        var run =
            paragraph.Append(
                text);

        run.FontSize(
            8.5f);

        run.Color(
            WordColors.Black);

        if (bold)
        {
            run.Bold();
        }

        paragraph.Alignment =
            Alignment.left;

        paragraph.SpacingAfter(
            5d);
    }


    //====================================================
    // INFORMATION PARAGRAPH
    //====================================================

    private static void AddCellParagraph(
        Cell cell,
        string text)
    {
        var paragraph =
            cell.InsertParagraph();

        paragraph.Append(
                text)
            .FontSize(8.5f)
            .Color(WordColors.Black);

        paragraph.Alignment =
            Alignment.left;

        paragraph.SpacingAfter(
            5d);
    }
}