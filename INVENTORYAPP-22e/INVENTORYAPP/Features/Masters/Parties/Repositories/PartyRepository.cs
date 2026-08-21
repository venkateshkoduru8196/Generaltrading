using INVENTORYAPP.Data;
using INVENTORYAPP.Features.Masters.Parties.Interfaces;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Masters.Parties.Repositories;

public class PartyRepository : IPartyRepository
{
    private readonly AppDbContext _context;

    public PartyRepository(AppDbContext context)
    {
        _context = context;
    }

    //==========================================
    // Get All
    //==========================================

    public async Task<List<Party>> GetAllAsync(
        int companyId)
    {
        return await _context.Parties
            .Where(x =>
                x.CompanyId == companyId &&
                x.IsActive &&
                !x.IsDeleted)
            .OrderBy(x => x.PartyName)
            .ToListAsync();
    }

    //==========================================
    // Get By Id
    //==========================================

    public async Task<Party?> GetByIdAsync(
        int companyId,
        int id)
    {
        return await _context.Parties
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.Id == id &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //==========================================
    // Get By Code
    //==========================================

    public async Task<Party?> GetByCodeAsync(
        int companyId,
        string partyCode)
    {
        return await _context.Parties
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.PartyCode == partyCode &&
                x.IsActive &&
                !x.IsDeleted);
    }


    //==========================================
    // Add
    //==========================================

    public async Task AddAsync(
        Party party)
    {
        await _context.Parties.AddAsync(party);
    }


    //==========================================
    // Update
    //==========================================

    public Task UpdateAsync(
        Party party)
    {
        _context.Parties.Update(party);

        return Task.CompletedTask;
    }


    //==========================================
    // Exists
    //==========================================

    public async Task<bool> ExistsAsync(
        int companyId,
        string partyCode)
    {
        return await _context.Parties
            .AnyAsync(x =>
                x.CompanyId == companyId &&
                x.PartyCode == partyCode &&
                x.IsActive &&
                !x.IsDeleted);
    }





    //==========================================
    // Delete
    //==========================================

    public Task DeleteAsync(
        Party party)
    {
        party.IsActive = false;
        party.IsDeleted = true;

        _context.Parties.Update(party);

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