using INVENTORYAPP.Data;
using INVENTORYAPP.Features.Masters.StockItems.Interfaces;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Masters.StockItems.Repositories;

public class StockItemRepository : IStockItemRepository
{
    private readonly AppDbContext _context;

    public StockItemRepository(AppDbContext context)
    {
        _context = context;
    }

    //==========================================
    // Get All
    //==========================================

    public async Task<List<StockItem>> GetAllAsync(int companyId)
    {
        return await _context.StockItems
            .Where(x =>
                x.CompanyId == companyId &&
                x.IsActive &&
                !x.IsDeleted)
            .OrderBy(x => x.StockName)
            .ToListAsync();
    }

    //==========================================
    // Get By Id
    //==========================================

    public async Task<StockItem?> GetByIdAsync(
        int companyId,
        int id)
    {
        return await _context.StockItems
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.Id == id &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //==========================================
    // Get By Stock Code
    //==========================================

    public async Task<StockItem?> GetByCodeAsync(
        int companyId,
        string stockCode)
    {
        return await _context.StockItems
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.StockCode == stockCode &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //==========================================
    // Exists
    //==========================================

    public async Task<bool> ExistsAsync(
        int companyId,
        string stockCode)
    {
        return await _context.StockItems
            .AnyAsync(x =>
                x.CompanyId == companyId &&
                x.StockCode == stockCode &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //==========================================
    // Add
    //==========================================

    public async Task AddAsync(StockItem stockItem)
    {
        await _context.StockItems.AddAsync(stockItem);
    }

    //==========================================
    // Update
    //==========================================

    public Task UpdateAsync(StockItem stockItem)
    {
        _context.StockItems.Update(stockItem);

        return Task.CompletedTask;
    }

    //==========================================
    // Soft Delete
    //==========================================

    public Task DeleteAsync(StockItem stockItem)
    {
        stockItem.IsActive = false;
        stockItem.IsDeleted = true;

        _context.StockItems.Update(stockItem);

        return Task.CompletedTask;
    }

    //==========================================
    // Save Changes
    //==========================================

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}