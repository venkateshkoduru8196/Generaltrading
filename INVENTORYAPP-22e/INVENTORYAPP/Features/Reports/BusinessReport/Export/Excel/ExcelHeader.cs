using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
using INVENTORYAPP.Shared.Excel;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Export.Excel;

public static class ExcelHeader
{
    //--------------------------------------------------------
    // Header
    //--------------------------------------------------------

    public static int Add(
        ExcelWorksheet sheet,
        BusinessReportRequestDto request)
    {
        int row = 1;

        //--------------------------------------------------------
        // Company Name
        //--------------------------------------------------------

        ExcelHelper.AddTitle(
            sheet,
            row,
            1,
            5,
            ExcelTheme.CompanyName);

        row++;

        //--------------------------------------------------------
        // Report Name
        //--------------------------------------------------------

        ExcelHelper.AddTitle(
            sheet,
            row,
            1,
            5,
            "BUSINESS REPORT");

        row++;

        //--------------------------------------------------------
        // Date Range
        //--------------------------------------------------------

        sheet.Cells[row, 1].Value = "From";

        sheet.Cells[row, 2].Value =
    request.FromDate.HasValue
        ? request.FromDate.Value.ToString("dd-MM-yyyy")
        : "";

        sheet.Cells[row, 3].Value = "To";


        sheet.Cells[row, 4].Value =
 request.ToDate.HasValue
     ? request.ToDate.Value.ToString("dd-MM-yyyy")
     : "";

        row++;

        //--------------------------------------------------------
        // Generated On
        //--------------------------------------------------------

        sheet.Cells[row, 1].Value =
            "Generated On";

        sheet.Cells[row, 2].Value =
            DateTime.Now.ToString("dd-MM-yyyy HH:mm");

        row += 2;

        //--------------------------------------------------------
        // Return next row
        //--------------------------------------------------------

        return row;
    }
}

