using INVENTORYAPP.Features.Masters.StockItems.DTOs;

namespace INVENTORYAPP.Features.Masters.StockItems.Interfaces;

public interface IStockItemService
{
    Task<List<StockItemResponse>> GetAllAsync();

    Task<StockItemResponse?> GetByIdAsync(int id);

    Task<List<StockItemLookupResponse>> GetLookupAsync();

    Task CreateAsync(CreateStockItemRequest request);

    Task UpdateAsync(UpdateStockItemRequest request);

    Task DeleteAsync(int id);
}

