using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;
using INVENTORYAPP.Shared.Word;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Export.Word;

public class BusinessReportWordService
    : IBusinessReportWordService
{
    private readonly IBusinessReportService _reportService;

    public BusinessReportWordService(
        IBusinessReportService reportService)
    {
        _reportService = reportService;
    }

    //--------------------------------------------------------
    // Generate Word
    //--------------------------------------------------------

    public async Task<byte[]> GenerateWordAsync(
        BusinessReportRequestDto request)
    {
        //--------------------------------------------------------
        // Get Report
        //--------------------------------------------------------

        var report =
            await _reportService.GetBusinessReportAsync(request);

        //--------------------------------------------------------
        // Create Document
        //--------------------------------------------------------

        using var document =
            WordDocumentBuilder.Create();

        //--------------------------------------------------------
        // Header
        //--------------------------------------------------------

        WordHeader.Add(
            document,
            request);

        //--------------------------------------------------------
        // Gold
        //--------------------------------------------------------

        BuildMetalSection(
            document,
            "GOLD",
            report.StockMovements
                  .Where(x => x.Metal == "Gold")
                  .ToList());

        //--------------------------------------------------------
        // Silver
        //--------------------------------------------------------

        BuildMetalSection(
            document,
            "SILVER",
            report.StockMovements
                  .Where(x => x.Metal == "Silver")
                  .ToList());

        //--------------------------------------------------------
        // Save
        //--------------------------------------------------------

        using var stream =
            new MemoryStream();

        document.SaveAs(stream);

        return stream.ToArray();
    }

    //--------------------------------------------------------
    // Build Metal Section
    //--------------------------------------------------------

    private static void BuildMetalSection(
        DocX document,
        string title,
        List<StockMovementRowDto> rows)
    {
        //--------------------------------------------------------
        // Skip Empty
        //--------------------------------------------------------

        if (!rows.Any())
            return;

        //--------------------------------------------------------
        // Heading
        //--------------------------------------------------------

        WordHelper.AddHeading(
            document,
            title);

        //--------------------------------------------------------
        // Table
        //--------------------------------------------------------

        var table =
            document.AddTable(
                rows.Count + 1,
                5);

        table.Design =
            TableDesign.TableGrid;

        //--------------------------------------------------------
        // Header
        //--------------------------------------------------------

        table.Rows[0].Cells[0].Paragraphs[0]
            .Append("Account").Bold();

        table.Rows[0].Cells[1].Paragraphs[0]
            .Append("Opening").Bold();

        table.Rows[0].Cells[2].Paragraphs[0]
            .Append("Move In").Bold();

        table.Rows[0].Cells[3].Paragraphs[0]
            .Append("Move Out").Bold();

        table.Rows[0].Cells[4].Paragraphs[0]
            .Append("Closing").Bold();

        //--------------------------------------------------------
        // Data
        //--------------------------------------------------------

        for (int i = 0; i < rows.Count; i++)
        {
            var item = rows[i];

            table.Rows[i + 1].Cells[0].Paragraphs[0]
                .Append(item.AccountName);

            table.Rows[i + 1].Cells[1].Paragraphs[0]
                .Append(item.Opening.ToString());

            table.Rows[i + 1].Cells[2].Paragraphs[0]
                .Append(item.MoveIn.ToString());

            table.Rows[i + 1].Cells[3].Paragraphs[0]
                .Append(item.MoveOut.ToString());

            table.Rows[i + 1].Cells[4].Paragraphs[0]
                .Append(item.Closing.ToString());
        }

        //--------------------------------------------------------
        // Insert Table
        //--------------------------------------------------------

        document.InsertTable(table);

        WordHelper.AddBlankLine(document);
    }
}