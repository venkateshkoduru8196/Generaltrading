using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.Accounts.Interfaces;

public interface IAccountRepository
{
    //==========================================
    // Get All
    //==========================================

    Task<List<Account>> GetAllAsync(
        int companyId);

    //==========================================
    // Get By Id
    //==========================================

    Task<Account?> GetByIdAsync(
        int companyId,
        int id);

    //==========================================
    // Get By Code
    //==========================================

    Task<Account?> GetByCodeAsync(
        int companyId,
        string accountCode);

    //==========================================
    // Exists
    //==========================================

    Task<bool> ExistsAsync(
        int companyId,
        string accountCode);

    //==========================================
    // Add
    //==========================================

    Task AddAsync(
        Account account);

    //==========================================
    // Update
    //==========================================

    Task UpdateAsync(
        Account account);

    //==========================================
    // Delete (Soft Delete)
    //==========================================

    Task DeleteAsync(
        Account account);

    //==========================================
    // Save Changes
    //==========================================

    Task SaveChangesAsync();
}