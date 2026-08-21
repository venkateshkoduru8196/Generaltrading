using INVENTORYAPP.Features.Masters.Items.DTOs;
using INVENTORYAPP.Features.Masters.Items.Interface;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.Masters.Items.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemController : ControllerBase
{
    private readonly IItemService _service;

    public ItemController(IItemService service)
    {
        _service = service;
    }

    // GET: api/item
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _service.GetAllAsync();

        return Ok(items);
    }

    // GET: api/item/1
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(long id)
    {
        var item = await _service.GetByIdAsync(id);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    // POST: api/item
    [HttpPost]
    public async Task<IActionResult> Create(
        ItemCreateDto dto)
    {
        var item = await _service.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = item.Id },
            item);
    }

    // PUT: api/item/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        long id,
        ItemCreateDto dto)
    {
        var item = await _service.UpdateAsync(id, dto);

        if (item == null)
            return NotFound();

        return Ok(item);
    }

    // DELETE: api/item/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(long id)
    {
        var result = await _service.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}