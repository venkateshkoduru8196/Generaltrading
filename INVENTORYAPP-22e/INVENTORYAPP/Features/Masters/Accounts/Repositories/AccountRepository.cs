using INVENTORYAPP.Data;
using INVENTORYAPP.Features.Masters.Accounts.Interfaces;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Masters.Accounts.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly AppDbContext _context;

    public AccountRepository(AppDbContext context)
    {
        _context = context;
    }

    //==========================================
    // Get All
    //==========================================

    public async Task<List<Account>> GetAllAsync(
        int companyId)
    {
        return await _context.Accounts
            .Where(x =>
                x.CompanyId == companyId &&
                x.IsActive &&
                !x.IsDeleted)
            .OrderBy(x => x.AccountName)
            .ToListAsync();
    }

    //==========================================
    // Get By Id
    //==========================================

    public async Task<Account?> GetByIdAsync(
        int companyId,
        int id)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.Id == id &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //==========================================
    // Get By Code
    //==========================================

    public async Task<Account?> GetByCodeAsync(
        int companyId,
        string accountCode)
    {
        return await _context.Accounts
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.AccountCode == accountCode &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //==========================================
    // Exists
    //==========================================

    public async Task<bool> ExistsAsync(
        int companyId,
        string accountCode)
    {
        return await _context.Accounts
            .AnyAsync(x =>
                x.CompanyId == companyId &&
                x.AccountCode == accountCode &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //==========================================
    // Add
    //==========================================

    public async Task AddAsync(Account account)
    {
        await _context.Accounts.AddAsync(account);
    }

    //==========================================
    // Update
    //==========================================

    public Task UpdateAsync(Account account)
    {
        _context.Accounts.Update(account);

        return Task.CompletedTask;
    }

    //==========================================
    // Delete (Soft Delete)
    //==========================================

    public Task DeleteAsync(Account account)
    {
        account.IsActive = false;
        account.IsDeleted = true;

        _context.Accounts.Update(account);

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