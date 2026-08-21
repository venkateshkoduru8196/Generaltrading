using INVENTORYAPP.Features.Masters.Parties.DTOs;
using INVENTORYAPP.Features.Masters.Parties.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.Masters.Parties.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PartyController : ControllerBase
{
    private readonly IPartyService _service;

    public PartyController(IPartyService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpGet("lookup")]
    public async Task<IActionResult> Lookup()
    {
        var result = await _service.GetLookupAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePartyRequest request)
    {
        var result = await _service.CreateAsync(request);

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdatePartyRequest request)
    {
        request.Id = id;

        var result = await _service.UpdateAsync(request);

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return NoContent();
    }
}