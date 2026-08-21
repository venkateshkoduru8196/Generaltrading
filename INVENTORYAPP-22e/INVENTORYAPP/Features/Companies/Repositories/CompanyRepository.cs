using INVENTORYAPP.Data;
using INVENTORYAPP.Features.Companies.Interfaces;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Companies.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Company>> GetAllAsync()
    {
        return await _context.Companies
            .Where(x => x.IsActive)
            .OrderBy(x => x.CompanyName)
            .ToListAsync();
    }

    public async Task<Company?> GetByIdAsync(int companyId)
    {
        return await _context.Companies
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.IsActive);
    }

    public async Task<Company?> GetByCodeAsync(string companyCode)
    {
        return await _context.Companies
            .FirstOrDefaultAsync(x =>
                x.CompanyCode == companyCode &&
                x.IsActive);
    }

    public async Task AddAsync(Company company)
    {
        await _context.Companies.AddAsync(company);
    }

    public Task UpdateAsync(Company company)
    {
        _context.Companies.Update(company);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Company company)
    {
        _context.Companies.Remove(company);
        return Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(string companyCode)
    {
        return await _context.Companies
            .AnyAsync(x =>
                x.CompanyCode == companyCode);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}