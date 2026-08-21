using INVENTORYAPP.Data;
using INVENTORYAPP.Features.Masters.Units.Interfaces;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Masters.Units.Repositories;

public class UnitRepository : IUnitRepository
{
    private readonly AppDbContext _context;

    public UnitRepository(AppDbContext context)
    {
        _context = context;
    }

    //==========================================
    // Get All
    //==========================================

    public async Task<List<Unit>> GetAllAsync(
        int companyId)
    {
        return await _context.Units
            .Where(x =>
                x.CompanyId == companyId &&
                x.IsActive &&
                !x.IsDeleted)
            .OrderBy(x => x.description)
            .ToListAsync();
    }

    //==========================================
    // Get By Id
    //==========================================

    public async Task<Unit?> GetByIdAsync(
        int companyId,
        int id)
    {
        return await _context.Units
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.Id == id &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //==========================================
    // Get By Code
    //==========================================

    public async Task<Unit?> GetByCodeAsync(
        int companyId,
        string code)
    {
        return await _context.Units
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.code == code &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //==========================================
    // Exists
    //==========================================

    public async Task<bool> ExistsAsync(
        int companyId,
        string code)
    {
        return await _context.Units
            .AnyAsync(x =>
                x.CompanyId == companyId &&
                x.code == code &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //==========================================
    // Add
    //==========================================

    public async Task AddAsync(Unit unit)
    {
        await _context.Units.AddAsync(unit);
    }

    //==========================================
    // Update
    //==========================================

    public Task UpdateAsync(Unit unit)
    {
        _context.Units.Update(unit);

        return Task.CompletedTask;
    }

    //==========================================
    // Delete (Soft Delete)
    //==========================================

    public Task DeleteAsync(Unit unit)
    {
        unit.IsActive = false;

        unit.IsDeleted = true;

        _context.Units.Update(unit);

        return Task.CompletedTask;
    }

    //==========================================
    // Save
    //==========================================

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}