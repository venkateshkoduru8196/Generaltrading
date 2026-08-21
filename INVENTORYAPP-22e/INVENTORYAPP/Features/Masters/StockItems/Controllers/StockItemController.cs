
//using INVENTORYAPP.Features.Masters.StockItems.DTOs;
//using INVENTORYAPP.Features.Masters.StockItems.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace INVENTORYAPP.Features.Masters.StockItems.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//[Authorize(Roles = "SuperAdmin,Admin")]
//public class StockItemController : ControllerBase
//{
//    private readonly IStockItemService _service;

//    public StockItemController(
//        IStockItemService service)
//    {
//        _service = service;
//    }

//    //==========================================
//    // Get All
//    //==========================================

//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        try
//        {
//            var result = await _service.GetAllAsync();

//            return Ok(result);
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(new
//            {
//                success = false,
//                message = ex.Message
//            });
//        }
//    }

//    //==========================================
//    // Get By Id
//    //==========================================

//    [HttpGet("{id:int}")]
//    public async Task<IActionResult> GetById(int id)
//    {
//        try
//        {
//            var result = await _service.GetByIdAsync(id);

//            if (result == null)
//            {
//                return NotFound(new
//                {
//                    success = false,
//                    message = "Stock Item not found."
//                });
//            }

//            return Ok(result);
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(new
//            {
//                success = false,
//                message = ex.Message
//            });
//        }
//    }

//    //==========================================
//    // Lookup
//    //==========================================

//    [HttpGet("lookup")]
//    public async Task<IActionResult> GetLookup()
//    {
//        try
//        {
//            var result = await _service.GetLookupAsync();

//            return Ok(result);
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(new
//            {
//                success = false,
//                message = ex.Message
//            });
//        }
//    }

//    //==========================================
//    // Create
//    //==========================================

//    [HttpPost]
//    public async Task<IActionResult> Create(
//        [FromBody] CreateStockItemRequest request)
//    {
//        try
//        {
//            await _service.CreateAsync(request);

//            return Ok(new
//            {
//                success = true,
//                message = "Stock Item created successfully."
//            });
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(new
//            {
//                success = false,
//                message = ex.Message
//            });
//        }
//    }

//    //==========================================
//    // Update
//    //==========================================

//    [HttpPut]
//    public async Task<IActionResult> Update(
//        [FromBody] UpdateStockItemRequest request)
//    {
//        try
//        {
//            await _service.UpdateAsync(request);

//            return Ok(new
//            {
//                success = true,
//                message = "Stock Item updated successfully."
//            });
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(new
//            {
//                success = false,
//                message = ex.Message
//            });
//        }
//    }

//    //==========================================
//    // Delete
//    //==========================================

//    [HttpDelete("{id:int}")]
//    public async Task<IActionResult> Delete(int id)
//    {
//        try
//        {
//            await _service.DeleteAsync(id);

//            return Ok(new
//            {
//                success = true,
//                message = "Stock Item deleted successfully."
//            });
//        }
//        catch (Exception ex)
//        {
//            return BadRequest(new
//            {
//                success = false,
//                message = ex.Message
//            });
//        }
//    }
//}



using INVENTORYAPP.Features.Masters.StockItems.DTOs;
using INVENTORYAPP.Features.Masters.StockItems.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.Masters.StockItems.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class StockItemController : ControllerBase
{
    private readonly IStockItemService _service;

    public StockItemController(
        IStockItemService service)
    {
        _service = service;
    }

    //==========================================
    // Get All
    //==========================================

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    //==========================================
    // Get By Id
    //==========================================

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Stock Item not found."
                });
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    //==========================================
    // Lookup
    //==========================================

    [HttpGet("lookup")]
    public async Task<IActionResult> GetLookup()
    {
        try
        {
            var result = await _service.GetLookupAsync();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    //==========================================
    // Create
    //==========================================

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateStockItemRequest request)
    {
        try
        {
            await _service.CreateAsync(request);

            return Ok(new
            {
                success = true,
                message = "Stock Item created successfully."
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    //==========================================
    // Update
    //==========================================

    [HttpPut]
    public async Task<IActionResult> Update(
        [FromBody] UpdateStockItemRequest request)
    {
        try
        {
            await _service.UpdateAsync(request);

            return Ok(new
            {
                success = true,
                message = "Stock Item updated successfully."
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }

    //==========================================
    // Delete
    //==========================================

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);

            return Ok(new
            {
                success = true,
                message = "Stock Item deleted successfully."
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                success = false,
                message = ex.Message
            });
        }
    }
}