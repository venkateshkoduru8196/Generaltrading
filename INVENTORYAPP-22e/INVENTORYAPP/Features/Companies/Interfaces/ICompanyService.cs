using INVENTORYAPP.Features.Companies.DTOs;

namespace INVENTORYAPP.Features.Companies.Interfaces;

public interface ICompanyService
{
    Task<List<CompanyResponse>> GetAllAsync();

    Task<CompanyResponse?> GetByIdAsync(int companyId);

    Task<List<CompanyLookupResponse>> GetLookupAsync();

    Task CreateAsync(CreateCompanyRequest request);

    Task UpdateAsync(UpdateCompanyRequest request);

    Task DeleteAsync(int companyId);
}