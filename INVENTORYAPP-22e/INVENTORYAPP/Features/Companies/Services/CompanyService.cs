using INVENTORYAPP.Features.Companies.DTOs;
using INVENTORYAPP.Features.Companies.Interfaces;
using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Companies.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;

    public CompanyService(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CompanyResponse>> GetAllAsync()
    {
        var companies = await _repository.GetAllAsync();

        return companies.Select(x => new CompanyResponse
        {
            CompanyId = x.CompanyId,
            CompanyCode = x.CompanyCode,
            CompanyName = x.CompanyName,
            OwnerName = x.OwnerName,
            GSTIN = x.GSTIN,
            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            Address = x.Address,
            IsActive = x.IsActive,
            CreatedOn = x.CreatedOn
        }).ToList();
    }

    public async Task<CompanyResponse?> GetByIdAsync(int companyId)
    {
        var company = await _repository.GetByIdAsync(companyId);

        if (company == null)
            return null;

        return new CompanyResponse
        {
            CompanyId = company.CompanyId,
            CompanyCode = company.CompanyCode,
            CompanyName = company.CompanyName,
            OwnerName = company.OwnerName,
            GSTIN = company.GSTIN,
            PhoneNumber = company.PhoneNumber,
            Email = company.Email,
            Address = company.Address,
            IsActive = company.IsActive,
            CreatedOn = company.CreatedOn
        };
    }

    public async Task<List<CompanyLookupResponse>> GetLookupAsync()
    {
        var companies = await _repository.GetAllAsync();

        return companies.Select(x => new CompanyLookupResponse
        {
            CompanyId = x.CompanyId,
            CompanyCode = x.CompanyCode,
            CompanyName = x.CompanyName
        }).ToList();
    }

    public async Task CreateAsync(CreateCompanyRequest request)
    {
        if (await _repository.ExistsAsync(request.CompanyCode))
            throw new Exception("Company Code already exists.");

        var company = new Company
        {
            CompanyCode = request.CompanyCode,
            CompanyName = request.CompanyName,
            OwnerName = request.OwnerName,
            GSTIN = request.GSTIN,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            Address = request.Address,
            IsActive = true,
            CreatedOn = DateTime.UtcNow
        };

        await _repository.AddAsync(company);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateCompanyRequest request)
    {
        var company = await _repository.GetByIdAsync(request.CompanyId);

        if (company == null)
            throw new Exception("Company not found.");

        var duplicate = await _repository.GetByCodeAsync(request.CompanyCode);

        if (duplicate != null &&
            duplicate.CompanyId != request.CompanyId)
        {
            throw new Exception("Company Code already exists.");
        }

        company.CompanyCode = request.CompanyCode;
        company.CompanyName = request.CompanyName;
        company.OwnerName = request.OwnerName;
        company.GSTIN = request.GSTIN;
        company.PhoneNumber = request.PhoneNumber;
        company.Email = request.Email;
        company.Address = request.Address;
        company.IsActive = request.IsActive;

        await _repository.UpdateAsync(company);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int companyId)
    {
        var company = await _repository.GetByIdAsync(companyId);

        if (company == null)
            throw new Exception("Company not found.");

        await _repository.DeleteAsync(company);
        await _repository.SaveChangesAsync();
    }
}