using INVENTORYAPP.Data;
using INVENTORYAPP.Features.Masters.Items.Interface;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Masters.Items.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly AppDbContext _context;

    public ItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<Item?> GetByIdAsync(long id)
    {
        return await _context.Items
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Item> CreateAsync(Item item)
    {
        await _context.Items.AddAsync(item);

        await _context.SaveChangesAsync();

        return item;
    }

    public async Task<Item?> UpdateAsync(Item item)
    {
        var existingItem = await _context.Items
            .FirstOrDefaultAsync(x => x.Id == item.Id);

        if (existingItem == null)
            return null;

        _context.Entry(existingItem)
            .CurrentValues
            .SetValues(item);

        await _context.SaveChangesAsync();

        return existingItem;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var item = await _context.Items
            .FirstOrDefaultAsync(x => x.Id == id);

        if (item == null)
            return false;

        _context.Items.Remove(item);

        await _context.SaveChangesAsync();

        return true;
    }
}