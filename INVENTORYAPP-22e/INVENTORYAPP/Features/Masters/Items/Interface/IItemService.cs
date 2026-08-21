using INVENTORYAPP.Features.Masters.Items.DTOs;
using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.Items.Interface;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAllAsync();

    Task<Item?> GetByIdAsync(long id);

    Task<Item> CreateAsync(ItemCreateDto dto);

    Task<Item?> UpdateAsync(long id, ItemCreateDto dto);

    Task<bool> DeleteAsync(long id);
}