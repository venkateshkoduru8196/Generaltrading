
using INVENTORYAPP.Features.Masters.Accounts.DTOs;
using INVENTORYAPP.Features.Masters.Accounts.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.Masters.Accounts.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SuperAdmin,Admin")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _service;

    public AccountController(
        IAccountService service)
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
                    message = "Account not found."
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
    public async Task<IActionResult> Lookup()
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
        [FromBody] CreateAccountRequest request)
    {
        try
        {
            var result = await _service.CreateAsync(request);

            return Ok(new
            {
                success = true,
                message = "Account created successfully.",
                data = result
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

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateAccountRequest request)
    {
        try
        {
            request.Id = id;

            var result = await _service.UpdateAsync(request);

            return Ok(new
            {
                success = true,
                message = "Account updated successfully.",
                data = result
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
                message = "Account deleted successfully."
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















//using INVENTORYAPP.Features.Masters.Accounts.DTOs;
//using INVENTORYAPP.Features.Masters.Accounts.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace INVENTORYAPP.Features.Masters.Accounts.Controllers;

//[ApiController]
//[Route("api/[controller]")]
//public class AccountController : ControllerBase
//{
//    private readonly IAccountService _service;

//    public AccountController(IAccountService service)
//    {
//        _service = service;
//    }

//    [HttpGet]
//    public async Task<IActionResult> GetAll()
//    {
//        var result = await _service.GetAllAsync();

//        return Ok(result);
//    }

//    [HttpGet("{id:int}")]
//    public async Task<IActionResult> GetById(int id)
//    {
//        var result = await _service.GetByIdAsync(id);

//        if (result == null)
//            return NotFound();

//        return Ok(result);
//    }

//    [HttpGet("lookup")]
//    public async Task<IActionResult> Lookup()
//    {
//        var result = await _service.GetLookupAsync();

//        return Ok(result);
//    }

//    [HttpPost]
//    public async Task<IActionResult> Create(CreateAccountRequest request)
//    {
//        var result = await _service.CreateAsync(request);

//        return Ok(result);
//    }

//    [HttpPut("{id:int}")]
//    public async Task<IActionResult> Update(
//        int id,
//        UpdateAccountRequest request)
//    {
//        request.Id = id;

//        var result = await _service.UpdateAsync(request);

//        return Ok(result);
//    }

//    [HttpDelete("{id:int}")]
//    public async Task<IActionResult> Delete(int id)
//    {
//        await _service.DeleteAsync(id);

//        return NoContent();
//    }
//} 