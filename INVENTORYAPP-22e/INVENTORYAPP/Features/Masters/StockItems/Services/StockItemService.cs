using INVENTORYAPP.Features.Masters.StockItems.DTOs;
using INVENTORYAPP.Features.Masters.StockItems.Interfaces;
using INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;
using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.StockItems.Services;

public class StockItemService : IStockItemService
{
    private readonly IStockItemRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public StockItemService(
        IStockItemRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    public async Task<List<StockItemResponse>> GetAllAsync()
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var stockItems = await _repository.GetAllAsync(companyId);

        return stockItems
            .Select(MapToResponse)
            .ToList();
    }

    public async Task<StockItemResponse?> GetByIdAsync(int id)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var stockItem = await _repository.GetByIdAsync(
            companyId,
            id);

        if (stockItem == null)
            return null;

        return MapToResponse(stockItem);
    }

    public async Task<List<StockItemLookupResponse>> GetLookupAsync()
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var stockItems = await _repository.GetAllAsync(companyId);

        return stockItems
            .Select(x => new StockItemLookupResponse
            {
                Id = x.Id,
                StockCode = x.StockCode,
                StockName = x.StockName,
                TaxRate = x.TaxRate
            })
            .ToList();
    }

    public async Task CreateAsync(CreateStockItemRequest request)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        if (await _repository.ExistsAsync(
            companyId,
            request.StockCode))
        {
            throw new Exception("Stock Code already exists.");
        }

        var stockItem = new StockItem
        {
            CompanyId = companyId,

            StockCode = request.StockCode,

            StockName = request.StockName,

            TaxRate = request.TaxRate,

            IsActive = true,

            IsDeleted = false,

            CreatedOn = DateTime.UtcNow,

            CreatedBy = _currentUser.UserName
        };

        await _repository.AddAsync(stockItem);

        await _repository.SaveChangesAsync();
    }


    public async Task UpdateAsync(UpdateStockItemRequest request)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var stockItem = await _repository.GetByIdAsync(
            companyId,
            request.Id);

        if (stockItem == null)
            throw new Exception("Stock Item not found.");

        var existing = await _repository.GetByCodeAsync(
            companyId,
            request.StockCode);

        if (existing != null &&
            existing.Id != request.Id)
        {
            throw new Exception("Stock Code already exists.");
        }

        stockItem.StockCode = request.StockCode;

        stockItem.StockName = request.StockName;

        stockItem.TaxRate = request.TaxRate;

        stockItem.IsActive = request.IsActive;

        stockItem.ModifiedOn = DateTime.UtcNow;

        stockItem.ModifiedBy = _currentUser.UserName;

        await _repository.UpdateAsync(stockItem);

        await _repository.SaveChangesAsync();
    }



    public async Task DeleteAsync(int id)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var stockItem = await _repository.GetByIdAsync(
            companyId,
            id);

        if (stockItem == null)
            throw new Exception("Stock Item not found.");

        stockItem.DeletedOn = DateTime.UtcNow;

        stockItem.DeletedBy = _currentUser.UserName;

        await _repository.DeleteAsync(stockItem);

        await _repository.SaveChangesAsync();
    }


private static StockItemResponse MapToResponse(
    StockItem stockItem)
    {
        return new StockItemResponse
        {
            Id = stockItem.Id,

            CompanyId = stockItem.CompanyId,

            StockCode = stockItem.StockCode,

            StockName = stockItem.StockName,

            TaxRate = stockItem.TaxRate,

            IsActive = stockItem.IsActive,

            IsDeleted = stockItem.IsDeleted,

            CreatedOn = stockItem.CreatedOn,

            CreatedBy = stockItem.CreatedBy,

            ModifiedOn = stockItem.ModifiedOn,

            ModifiedBy = stockItem.ModifiedBy,

            DeletedOn = stockItem.DeletedOn,

            DeletedBy = stockItem.DeletedBy
        };



    }

}


