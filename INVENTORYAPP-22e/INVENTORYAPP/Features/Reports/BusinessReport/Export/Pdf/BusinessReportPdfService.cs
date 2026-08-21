using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;

using INVENTORYAPP.Shared.Pdf;
using iText.Layout;
using iText.Layout.Element;
namespace INVENTORYAPP.Features.Reports.BusinessReport.Export.Pdf;

public class BusinessReportPdfService : IBusinessReportPdfService
{
    private readonly IBusinessReportService _reportService;

    public BusinessReportPdfService(
        IBusinessReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<byte[]> GeneratePdfAsync(
        BusinessReportRequestDto request)
    {
        //--------------------------------------------------------
        // Get Report Data
        //--------------------------------------------------------

        var report =
            await _reportService.GetBusinessReportAsync(request);

        //--------------------------------------------------------
        // Memory Stream
        //--------------------------------------------------------

        using var stream = new MemoryStream();

        //--------------------------------------------------------
        // Create Document
        //--------------------------------------------------------

        var document =
            PdfDocumentBuilder.Create(stream);

        //--------------------------------------------------------
        // Header
        //--------------------------------------------------------

        PdfHeader.Add(document, request);

        //--------------------------------------------------------
        // Gold Section
        //--------------------------------------------------------

        BuildMetalTable(
            document,
            "GOLD",
            report.StockMovements
                .Where(x => x.Metal == "Gold")
                .ToList());

        //--------------------------------------------------------
        // Space
        //--------------------------------------------------------

        document.Add(new Paragraph(" "));

        //--------------------------------------------------------
        // Silver Section
        //--------------------------------------------------------

        BuildMetalTable(
            document,
            "SILVER",
            report.StockMovements
                .Where(x => x.Metal == "Silver")
                .ToList());

        //--------------------------------------------------------
        // Footer
        //--------------------------------------------------------

        PdfFooter.Add(document);

        //--------------------------------------------------------
        // Close
        //--------------------------------------------------------

        document.Close();

        return stream.ToArray();
    }

    //--------------------------------------------------------
    // Build Metal Table
    //--------------------------------------------------------

    private void BuildMetalTable(
        Document document,
        string title,
        List<StockMovementRowDto> rows)
    {
        //--------------------------------------------------------
        // Skip if no data
        //--------------------------------------------------------

        if (!rows.Any())
            return;

        //--------------------------------------------------------
        // Section Title
        //--------------------------------------------------------

        document.Add(

            PdfHelper.Paragraph(
                title,
                14,
                true,
                PdfColors.Primary)

        );

        //--------------------------------------------------------
        // Table
        //--------------------------------------------------------

        var table = PdfTable.Create(
            3,
            2,
            2,
            2,
            2);

        //--------------------------------------------------------
        // Header
        //--------------------------------------------------------

        PdfTable.AddHeader(
            table,
            "Account",
            "Opening",
            "Move In",
            "Move Out",
            "Closing");

        //--------------------------------------------------------
        // Rows
        //--------------------------------------------------------

        bool alternate = false;

        foreach (var row in rows)
        {
            bool total =
                row.AccountName.Equals(
                    "Total",
                    StringComparison.OrdinalIgnoreCase);

            if (total)
            {
                PdfTable.AddTotal(table, row.AccountName);
                PdfTable.AddTotal(table, row.Opening);
                PdfTable.AddTotal(table, row.MoveIn);
                PdfTable.AddTotal(table, row.MoveOut);
                PdfTable.AddTotal(table, row.Closing);
            }
            else
            {
                PdfTable.AddText(
                    table,
                    row.AccountName,
                    alternate);

                PdfTable.AddNumber(
                    table,
                    row.Opening,
                    alternate);

                PdfTable.AddNumber(
                    table,
                    row.MoveIn,
                    alternate);

                PdfTable.AddNumber(
                    table,
                    row.MoveOut,
                    alternate);

                PdfTable.AddNumber(
                    table,
                    row.Closing,
                    alternate);
            }

            alternate = !alternate;
        }

        //--------------------------------------------------------
        // Add Table
        //--------------------------------------------------------

        document.Add(table);

        //--------------------------------------------------------
        // Space
        //--------------------------------------------------------

        document.Add(new Paragraph(""));
    }
}