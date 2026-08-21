using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.Items.Interface;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetAllAsync();

    Task<Item?> GetByIdAsync(long id);

    Task<Item> CreateAsync(Item item);

    Task<Item?> UpdateAsync(Item item);

    Task<bool> DeleteAsync(long id);
}