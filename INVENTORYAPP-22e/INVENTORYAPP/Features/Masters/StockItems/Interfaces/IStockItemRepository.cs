using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.StockItems.Interfaces;

public interface IStockItemRepository
{
    Task<List<StockItem>> GetAllAsync(int companyId);

    Task<StockItem?> GetByIdAsync(
        int companyId,
        int id);

    Task<StockItem?> GetByCodeAsync(
        int companyId,
        string stockCode);

    Task<bool> ExistsAsync(
        int companyId,
        string stockCode);

    Task AddAsync(StockItem stockItem);

    Task UpdateAsync(StockItem stockItem);

    Task DeleteAsync(StockItem stockItem);

    Task SaveChangesAsync();
}