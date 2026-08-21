using INVENTORYAPP.Features.Companies.DTOs;
using INVENTORYAPP.Features.Companies.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace INVENTORYAPP.Features.Companies.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "SuperAdmin")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _service;

    public CompanyController(ICompanyService service)
    {
        _service = service;
    }

    //==========================================
    // Get All Companies
    //==========================================

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();

        return Ok(result);
    }

    //==========================================
    // Get Company By Id
    //==========================================

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    //==========================================
    // Company Lookup
    //==========================================

    [HttpGet("lookup")]
    public async Task<IActionResult> Lookup()
    {
        var result = await _service.GetLookupAsync();

        return Ok(result);
    }

    //==========================================
    // Create Company
    //==========================================

    [HttpPost]
    public async Task<IActionResult> Create(CreateCompanyRequest request)
    {
        await _service.CreateAsync(request);

        return Ok(new
        {
            Message = "Company created successfully."
        });
    }

    //==========================================
    // Update Company
    //==========================================

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCompanyRequest request)
    {
        await _service.UpdateAsync(request);

        return Ok(new
        {
            Message = "Company updated successfully."
        });
    }

    //==========================================
    // Delete Company
    //==========================================

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);

        return Ok(new
        {
            Message = "Company deleted successfully."
        });
    }
}