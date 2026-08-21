using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;
using INVENTORYAPP.Shared.Excel;
using OfficeOpenXml;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Export.Excel;

public class BusinessReportExcelService : IBusinessReportExcelService
{
    private readonly IBusinessReportService _reportService;

    public BusinessReportExcelService(
        IBusinessReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<byte[]> GenerateExcelAsync(
        BusinessReportRequestDto request)
    {
        var report =
            await _reportService.GetBusinessReportAsync(request);

        using var package =
            ExcelDocumentBuilder.Create("Business Report");

        var sheet =
            package.Workbook.Worksheets[0];

        int row =
            ExcelHeader.Add(sheet, request);

        row = BuildMetalTable(
            sheet,
            row,
            "GOLD",
            report.StockMovements
                .Where(x => x.Metal == "Gold")
                .ToList());

        row += 2;

        row = BuildMetalTable(
            sheet,
            row,
            "SILVER",
            report.StockMovements
                .Where(x => x.Metal == "Silver")
                .ToList());

        ExcelHelper.AutoFit(sheet);

        return package.GetAsByteArray();
    }

    //--------------------------------------------------------
    // Build Metal Table
    //--------------------------------------------------------

    private int BuildMetalTable(
       ExcelWorksheet sheet,
       int row,
       string title,
       List<StockMovementRowDto> rows)
    {
        //--------------------------------------------------------
        // Skip if no data
        //--------------------------------------------------------

        if (!rows.Any())
            return row;

        //--------------------------------------------------------
        // Section Title
        //--------------------------------------------------------

        ExcelHelper.AddTitle(
            sheet,
            row,
            1,
            5,
            title);

        row++;

        //--------------------------------------------------------
        // Table Header
        //--------------------------------------------------------

        ExcelHelper.AddHeader(sheet, row, 1, "Account");
        ExcelHelper.AddHeader(sheet, row, 2, "Opening");
        ExcelHelper.AddHeader(sheet, row, 3, "Move In");
        ExcelHelper.AddHeader(sheet, row, 4, "Move Out");
        ExcelHelper.AddHeader(sheet, row, 5, "Closing");

        row++;

        //--------------------------------------------------------
        // Data Rows
        //--------------------------------------------------------

        foreach (var item in rows)
        {
            ExcelHelper.AddCell(
                sheet,
                row,
                1,
                item.AccountName);

            ExcelHelper.AddCell(
                sheet,
                row,
                2,
                item.Opening);

            ExcelHelper.AddCell(
                sheet,
                row,
                3,
                item.MoveIn);

            ExcelHelper.AddCell(
                sheet,
                row,
                4,
                item.MoveOut);

            ExcelHelper.AddCell(
                sheet,
                row,
                5,
                item.Closing);

            row++;
        }

        //--------------------------------------------------------
        // Next Row
        //--------------------------------------------------------

        return row;
    }
}

