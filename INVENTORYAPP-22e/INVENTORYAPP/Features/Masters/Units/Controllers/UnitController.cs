using INVENTORYAPP.Features.Masters.Units.DTOs;
using INVENTORYAPP.Features.Masters.Units.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.Masters.Units.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class UnitController : ControllerBase
{
    private readonly IUnitService _service;

    public UnitController(
        IUnitService service)
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
            var result =
                await _service.GetAllAsync();

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
    public async Task<IActionResult> GetById(
        int id)
    {
        try
        {
            var result =
                await _service.GetByIdAsync(id);

            if (result == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "Unit not found."
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
            var result =
                await _service.GetLookupAsync();

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
        [FromBody] CreateUnitRequest request)
    {
        try
        {
            await _service.CreateAsync(request);

            return Ok(new
            {
                success = true,
                message = "Unit created successfully."
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
        [FromBody] UpdateUnitRequest request)
    {
        try
        {
            await _service.UpdateAsync(request);

            return Ok(new
            {
                success = true,
                message = "Unit updated successfully."
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
    public async Task<IActionResult> Delete(
        int id)
    {
        try
        {
            await _service.DeleteAsync(id);

            return Ok(new
            {
                success = true,
                message = "Unit deleted successfully."
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









//using INVENTORYAPP.Features.Masters.Units.DTOs;
//using INVENTORYAPP.Features.Masters.Units.Interfaces;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace INVENTORYAPP.Features.Masters.Units.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//[Authorize]
//public class UnitController : ControllerBase
//{
//    private readonly IUnitService _service;

//    public UnitController(
//        IUnitService service)
//    {
//        _service = service;
//    }

//    //==========================================
//    // Get All
//    //==========================================

//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        var result =
//            await _service.GetAllAsync();

//        return Ok(result);
//    }

//    //==========================================
//    // Get By Id
//    //==========================================

//    [HttpGet("{id:int}")]
//    public async Task<IActionResult> GetById(
//        int id)
//    {
//        var result =
//            await _service.GetByIdAsync(id);

//        if (result == null)
//        {
//            return NotFound(new
//            {
//                Message = "Unit not found."
//            });
//        }

//        return Ok(result);
//    }

//    //==========================================
//    // Lookup
//    //==========================================

//    [HttpGet("lookup")]
//    public async Task<IActionResult> GetLookup()
//    {
//        var result =
//            await _service.GetLookupAsync();

//        return Ok(result);
//    }

//    //==========================================
//    // Create
//    //==========================================

//    [HttpPost]
//    public async Task<IActionResult> Create(
//        [FromBody] CreateUnitRequest request)
//    {
//        await _service.CreateAsync(request);

//        return Ok(new
//        {
//            Message = "Unit created successfully."
//        });
//    }

//    //==========================================
//    // Update
//    //==========================================

//    [HttpPut]
//    public async Task<IActionResult> Update(
//        [FromBody] UpdateUnitRequest request)
//    {
//        await _service.UpdateAsync(request);

//        return Ok(new
//        {
//            Message = "Unit updated successfully."
//        });
//    }

//    //==========================================
//    // Delete
//    //==========================================

//    [HttpDelete("{id:int}")]
//    public async Task<IActionResult> Delete(
//        int id)
//    {
//        await _service.DeleteAsync(id);

//        return Ok(new
//        {
//            Message = "Unit deleted successfully."
//        });
//    }
//}