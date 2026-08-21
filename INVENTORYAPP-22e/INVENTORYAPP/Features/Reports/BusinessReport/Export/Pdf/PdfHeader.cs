using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
using INVENTORYAPP.Shared.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Export.Pdf;

public static class PdfHeader
{
    public static void Add(
        Document document,
        BusinessReportRequestDto request)
    {
        //------------------------------------------------------
        // Company Name
        //------------------------------------------------------

        document.Add(

            PdfHelper.Paragraph(
                PdfTheme.CompanyName,
                PdfTheme.TitleFont,
                true,
                PdfColors.Primary)

            .SetTextAlignment(TextAlignment.CENTER)

        );

        //------------------------------------------------------
        // Company Address
        //------------------------------------------------------

        document.Add(

            PdfHelper.Paragraph(
                PdfTheme.CompanyAddress,
                PdfTheme.SmallFont)

            .SetTextAlignment(TextAlignment.CENTER)

        );

        //------------------------------------------------------
        // Report Title
        //------------------------------------------------------

        document.Add(

            PdfHelper.Paragraph(
                "BUSINESS REPORT",
                PdfTheme.HeaderFont,
                true)

            .SetTextAlignment(TextAlignment.CENTER)

        );

        //------------------------------------------------------
        // Blank Line
        //------------------------------------------------------

        document.Add(new Paragraph(" "));

        //------------------------------------------------------
        // Report Information Table
        //------------------------------------------------------

        var infoTable = new Table(UnitValue.CreatePercentArray(new float[] { 50, 50 }))
            .UseAllAvailableWidth();

        infoTable.AddCell(
            PdfHelper.Cell(
                $"Report Type : {request.ReportType}",
                true));

        infoTable.AddCell(
            PdfHelper.Cell(
                $"Generated : {DateTime.Now:dd-MM-yyyy HH:mm}",
                true,
                null,
                TextAlignment.RIGHT));

        document.Add(infoTable);

        document.Add(new Paragraph(" "));
    }
}