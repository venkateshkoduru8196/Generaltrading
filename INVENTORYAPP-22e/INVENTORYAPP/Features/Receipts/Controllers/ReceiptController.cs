using INVENTORYAPP.Features.Receipts.DTOs;
using INVENTORYAPP.Features.Receipts.Interface;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.Receipts.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReceiptController : ControllerBase
{
    private readonly IReceiptService _receiptService;

    public ReceiptController(IReceiptService receiptService)
    {
        _receiptService = receiptService;
    }

    [HttpGet("parties")]
    public async Task<IActionResult> GetParties()
    {
        var parties = await _receiptService.GetPartiesAsync();
        return Ok(parties);
    }

    [HttpGet("accounts")]
    public async Task<IActionResult> GetAccounts()
    {
        var accounts = await _receiptService.GetAccountsAsync();
        return Ok(accounts);
    }

    [HttpPost]
    public async Task<IActionResult> SaveReceipt([FromBody] SaveReceiptDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _receiptService.SaveReceiptAsync(dto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string keyword)
    {
        var result = await _receiptService.SearchAsync(keyword);
        return Ok(result);
    }

    [HttpGet("next-number")]
    public async Task<IActionResult> GetNextReceiptNumber()
    {
        var nextNumber = await _receiptService.GetNextReceiptNumberAsync();
        return Ok(nextNumber);
    }

    [HttpGet("{docNo}")]
    public async Task<IActionResult> GetReceipt(long docNo)
    {
        var receipt = await _receiptService.GetReceiptByDocNoAsync(docNo);

        if (receipt == null)
        {
            return NotFound();
        }

        return Ok(receipt);
    }

    [HttpDelete("{docNo}")]
    public async Task<IActionResult> DeleteReceipt(long docNo)
    {
        await _receiptService.DeleteReceiptAsync(docNo);

        return Ok(new
        {
            Message = "Receipt deleted successfully."
        });
    }
}
