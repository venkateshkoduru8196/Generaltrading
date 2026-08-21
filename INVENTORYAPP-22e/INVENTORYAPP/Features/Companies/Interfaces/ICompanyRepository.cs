using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Companies.Interfaces;

public interface ICompanyRepository
{
    Task<List<Company>> GetAllAsync();

    Task<Company?> GetByIdAsync(int companyId);

    Task<Company?> GetByCodeAsync(string companyCode);

    Task AddAsync(Company company);

    Task UpdateAsync(Company company);

    Task DeleteAsync(Company company);

    Task<bool> ExistsAsync(string companyCode);

    Task SaveChangesAsync();
}