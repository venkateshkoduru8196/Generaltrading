using INVENTORYAPP.Features.Sales.DTOs;
using INVENTORYAPP.Features.Sales.Interfaces;
using INVENTORYAPP.Features.Sales.Interfaces.Export;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.Sales.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GSalController : ControllerBase
{
    //=====================================================
    // SERVICES
    //=====================================================

    private readonly IGSalService _service;

    private readonly IGSalPdfService _pdfService;

    private readonly IGSalWordService _wordService;

    private readonly IGSalExcelService _excelService;


    //=====================================================
    // CONSTRUCTOR
    //=====================================================

    public GSalController(
        IGSalService service,
        IGSalPdfService pdfService,
        IGSalWordService wordService,
        IGSalExcelService excelService)
    {
        _service = service;

        _pdfService = pdfService;

        _wordService = wordService;

        _excelService = excelService;
    }


    //=====================================================
    // CREATE SALES INVOICE
    //=====================================================

    [HttpPost]
    public async Task<IActionResult> Create(
        GSalCreateRequestDto request)
    {
        var result =
            await _service.CreateAsync(request);

        return Ok(result);
    }


    //=====================================================
    // GET SALES BY ID
    //=====================================================

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(
        int id)
    {
        var result =
            await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    //=====================================================
    // GET SALES BY DOCUMENT NUMBER
    //=====================================================

    [HttpGet("doc/{docNo}")]
    public async Task<IActionResult> GetByDocNo(
        string docNo)
    {
        var result =
            await _service.GetByDocNoAsync(docNo);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    //=====================================================
    // GET ALL SALES OF LOGGED-IN COMPANY
    //=====================================================

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result =
            await _service.GetAllAsync();

        return Ok(result);
    }


    //=====================================================
    // UPDATE SALES INVOICE
    //=====================================================

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        GSalCreateRequestDto request)
    {
        await _service.UpdateAsync(
            id,
            request);

        return Ok(new
        {
            Message =
                "Sales Invoice updated successfully."
        });
    }


    //=====================================================
    // DELETE SALES INVOICE
    //=====================================================

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(
        int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message =
                "Sales Invoice deleted successfully."
        });
    }


    //=====================================================
    // VIEW PDF
    //=====================================================

    [HttpGet("{id:int}/view/pdf")]
    public async Task<IActionResult> ViewPdf(
        int id)
    {
        var pdf =
            await _pdfService.GeneratePdfAsync(id);

        return File(
            pdf,
            "application/pdf");
    }


    //=====================================================
    // DOWNLOAD PDF
    //=====================================================

    [HttpGet("{id:int}/download/pdf")]
    public async Task<IActionResult> DownloadPdf(
        int id)
    {
        var pdf =
            await _pdfService.GeneratePdfAsync(id);

        return File(
            pdf,
            "application/pdf",
            $"SalesInvoice_{id}.pdf");
    }


    //=====================================================
    // DOWNLOAD WORD
    //=====================================================

    [HttpGet("{id:int}/download/word")]
    public async Task<IActionResult> DownloadWord(
        int id)
    {
        var word =
            await _wordService.GenerateWordAsync(id);

        return File(
            word,
            "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            $"SalesInvoice_{id}.docx");
    }


    //=====================================================
    // DOWNLOAD EXCEL
    //=====================================================

    [HttpGet("{id:int}/download/excel")]
    public async Task<IActionResult> DownloadExcel(
        int id)
    {
        var excel =
            await _excelService.GenerateExcelAsync(id);

        return File(
            excel,
            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            $"SalesInvoice_{id}.xlsx");
    }


    //=====================================================
    // TEMPORARY - VIEW EXPORT DATA
    //=====================================================

    //[HttpGet("{id:int}/export-data")]
    //public async Task<IActionResult> GetExportData(
    //    int id)
    //{
    //    var invoice =
    //        await _service.GetInvoiceForExportAsync(id);

    //    if (invoice == null)
    //        return NotFound();

    //    return Ok(invoice);
    //}
}