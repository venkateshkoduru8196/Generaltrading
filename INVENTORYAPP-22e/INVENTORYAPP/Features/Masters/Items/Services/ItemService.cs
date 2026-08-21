using INVENTORYAPP.Features.Masters.Items.DTOs;
using INVENTORYAPP.Features.Masters.Items.Interface;
using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.Items.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _repository;

    public ItemService(IItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Item?> GetByIdAsync(long id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Item> CreateAsync(ItemCreateDto dto)
    {
        var item = new Item
        {
            Code = dto.Code,
            Name = dto.Name,
            RegionalName = dto.RegionalName,
            HsnCode = dto.HsnCode,
            CgstPer = dto.CgstPer,
            SgstPer = dto.SgstPer,
            IgstPer = dto.IgstPer,
            PRate = dto.PRate,
            SRate = dto.SRate,
            Mrp = dto.Mrp,
            IsExpiry = dto.IsExpiry,
            Remarks = dto.Remarks
        };

        return await _repository.CreateAsync(item);
    }

    public async Task<Item?> UpdateAsync(long id, ItemCreateDto dto)
    {
        var item = await _repository.GetByIdAsync(id);

        if (item == null)
            return null;

        item.Code = dto.Code;
        item.Name = dto.Name;
        item.RegionalName = dto.RegionalName;
        item.HsnCode = dto.HsnCode;
        item.CgstPer = dto.CgstPer;
        item.SgstPer = dto.SgstPer;
        item.IgstPer = dto.IgstPer;
        item.PRate = dto.PRate;
        item.SRate = dto.SRate;
        item.Mrp = dto.Mrp;
        item.IsExpiry = dto.IsExpiry;
        item.Remarks = dto.Remarks;

        return await _repository.UpdateAsync(item);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        return await _repository.DeleteAsync(id);
    }
}