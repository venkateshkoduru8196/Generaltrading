using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
using INVENTORYAPP.Shared.Word;
using Xceed.Words.NET;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Export.Word;

public static class WordHeader
{
    //--------------------------------------------------------
    // Header
    //--------------------------------------------------------

    public static void Add(
        DocX document,
        BusinessReportRequestDto request)
    {
        //--------------------------------------------------------
        // Company
        //--------------------------------------------------------

        WordHelper.AddCompany(document);

        //--------------------------------------------------------
        // Report Title
        //--------------------------------------------------------

        WordHelper.AddTitle(
            document,
            WordTheme.ReportTitle);

        //--------------------------------------------------------
        // Blank Line
        //--------------------------------------------------------

        WordHelper.AddBlankLine(document);

        //--------------------------------------------------------
        // From Date
        //--------------------------------------------------------

        WordHelper.AddParagraph(
            document,
            $"From : {(request.FromDate.HasValue
                ? request.FromDate.Value.ToString("dd-MM-yyyy")
                : "-")}");

        //--------------------------------------------------------
        // To Date
        //--------------------------------------------------------

        WordHelper.AddParagraph(
            document,
            $"To : {(request.ToDate.HasValue
                ? request.ToDate.Value.ToString("dd-MM-yyyy")
                : "-")}");

        //--------------------------------------------------------
        // Generated On
        //--------------------------------------------------------

        WordHelper.AddParagraph(
            document,
            $"Generated On : {DateTime.Now:dd-MM-yyyy HH:mm}");

        //--------------------------------------------------------
        // Blank Line
        //--------------------------------------------------------

        WordHelper.AddBlankLine(document);
    }
}