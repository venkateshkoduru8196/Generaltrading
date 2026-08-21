using INVENTORYAPP.Features.Payments.DTOs;
using INVENTORYAPP.Features.Payments.Interface;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.Payments.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("parties")]
    public async Task<IActionResult> GetParties()
    {
        var parties = await _paymentService.GetPartiesAsync();
        return Ok(parties);
    }

    [HttpGet("accounts")]
    public async Task<IActionResult> GetAccounts()
    {
        var accounts = await _paymentService.GetAccountsAsync();
        return Ok(accounts);
    }

    [HttpPost]
    public async Task<IActionResult> SavePayment([FromBody] SavePaymentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _paymentService.SavePaymentAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<IActionResult> Search(string keyword)
    {
        var result = await _paymentService.SearchAsync(keyword);
        return Ok(result);
    }

    [HttpGet("next-number")]
    public async Task<IActionResult> GetNextPaymentNumber()
    {
        var nextNumber = await _paymentService.GetNextPaymentNumberAsync();
        return Ok(nextNumber);
    }

    [HttpGet("{docNo}")]
    public async Task<IActionResult> GetPayment(long docNo)
    {
        var payment = await _paymentService.GetPaymentByDocNoAsync(docNo);

        if (payment == null)
            return NotFound();

        return Ok(payment);
    }

    [HttpDelete("{docNo}")]
    public async Task<IActionResult> DeletePayment(long docNo)
    {
        await _paymentService.DeletePaymentAsync(docNo);

        return Ok(new
        {
            Message = "Payment deleted successfully."
        });
    }
}
