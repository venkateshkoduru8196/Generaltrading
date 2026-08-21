using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.Parties.Interfaces;

public interface IPartyRepository
{
    // Get All
    Task<List<Party>> GetAllAsync(int companyId);

    // Get By Id
    Task<Party?> GetByIdAsync(int companyId, int id);

    // Get By Code
    Task<Party?> GetByCodeAsync(int companyId, string partyCode);

    // Exists
    Task<bool> ExistsAsync(int companyId, string partyCode);

    // Add
    Task AddAsync(Party party);

    // Update
    Task UpdateAsync(Party party);

    // Delete
    Task DeleteAsync(Party party);

    // Save
    Task SaveChangesAsync();
}