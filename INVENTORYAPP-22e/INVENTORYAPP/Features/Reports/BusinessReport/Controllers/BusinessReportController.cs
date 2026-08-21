using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using INVENTORYAPP.Features.Reports.BusinessReport.DTOs;
using INVENTORYAPP.Features.Reports.BusinessReport.Interfaces;

namespace INVENTORYAPP.Features.Reports.BusinessReport.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "SuperAdmin,Admin")]
public class BusinessReportController : ControllerBase
{
    private readonly IBusinessReportService _businessReportService;

    private readonly IBusinessReportPdfService _pdfService;
    public BusinessReportController(
        IBusinessReportService businessReportService, IBusinessReportPdfService pdfService)
    {
        _businessReportService = businessReportService;

        _pdfService = pdfService;
    }



    [HttpPost]
    public async Task<IActionResult> GetBusinessReport(
    [FromBody] BusinessReportRequestDto request)

//[HttpPost]
    //public async Task<IActionResult> GetBusinessReport(
    //    BusinessReportRequestDto request)
    {
        var result = await _businessReportService
            .GetBusinessReportAsync(request);

        return Ok(result);
    }


    //[HttpPost("pdf")]
    //public async Task<IActionResult> DownloadPdf(
    //BusinessReportRequestDto request)
    //{
    //    var pdf = await _pdfService.GeneratePdfAsync(request);

    //    return File(
    //        pdf,
    //        "application/pdf",
    //        "BusinessReport.pdf");
    //}






}