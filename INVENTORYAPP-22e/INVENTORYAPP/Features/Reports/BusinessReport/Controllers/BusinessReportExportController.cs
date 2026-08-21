using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BusinessReportExportController : ControllerBase
{
    private readonly IBusinessReportPdfService _pdfService;
    private readonly IBusinessReportExcelService _excelService;
    private readonly IBusinessReportWordService _wordService;

    public BusinessReportExportController(
        IBusinessReportPdfService pdfService,
        IBusinessReportExcelService excelService,
        IBusinessReportWordService wordService)
    {
        _pdfService = pdfService;
        _excelService = excelService;
        _wordService = wordService;
    }

    //--------------------------------------------------------
    // View PDF
    //--------------------------------------------------------

    [HttpPost("view")]
    public async Task<IActionResult> ViewPdf(
        [FromBody] BusinessReportRequestDto request)
    {
        var pdf = await _pdfService.GeneratePdfAsync(request);

        return File(
            pdf,
            "application/pdf");
    }

    //--------------------------------------------------------
    // Download PDF
    //--------------------------------------------------------

    [HttpPost("download")]
    public async Task<IActionResult> DownloadPdf(
        [FromBody] BusinessReportRequestDto request)
    {
        var pdf = await _pdfService.GeneratePdfAsync(request);

        var fileName =
            $"BusinessReport_{DateTime.Now:yyyyMMddHHmmss}.pdf";

        return File(
            pdf,
            "application/pdf",
            fileName);
    }

    //--------------------------------------------------------
    // Download Excel
    //--------------------------------------------------------

    [HttpPost("excel")]
    public async Task<IActionResult> DownloadExcel(
        [FromBody] BusinessReportRequestDto request)
    {
        var excel =
            await _excelService.GenerateExcelAsync(request);

        var fileName =
            $"BusinessReport_{DateTime.Now:yyyyMMddHHmmss}.xlsx";

        return File(
            excel,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileName);
    }

    //--------------------------------------------------------
    // Download Word
    //--------------------------------------------------------

    [HttpPost("word")]
    public async Task<IActionResult> DownloadWord(
        [FromBody] BusinessReportRequestDto request)
    {
        var word =
            await _wordService.GenerateWordAsync(request);

        var fileName =
            $"BusinessReport_{DateTime.Now:yyyyMMddHHmmss}.docx";

        return File(
            word,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            fileName);
    }
}