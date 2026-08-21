using INVENTORYAPP.Data;
using INVENTORYAPP.Features.Sales.Interfaces;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Sales.Repositories;

public class GSalRepository : IGSalRepository
{
    private readonly AppDbContext _context;

    public GSalRepository(AppDbContext context)
    {
        _context = context;
    }

    //=====================================================
    // CREATE
    //=====================================================

    public async Task<GSal> AddAsync(GSal sale)
    {
        await _context.GSales.AddAsync(sale);
        return sale;
    }

    //=====================================================
    // GET BY ID
    // CompanyId + Invoice Id
    //=====================================================

    public async Task<GSal?> GetByIdAsync(
        int companyId,
        int id)
    {
        return await _context.GSales
            .Include(x => x.Details
                .Where(d => d.IsActive && !d.IsDeleted))
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.Id == id &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //=====================================================
    // GET BY DOCUMENT NUMBER
    // CompanyId + DocNo
    //=====================================================

    public async Task<GSal?> GetByDocNoAsync(
        int companyId,
        string docNo)
    {
        return await _context.GSales
            .Include(x => x.Details
                .Where(d => d.IsActive && !d.IsDeleted))
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.docno == docNo &&
                x.IsActive &&
                !x.IsDeleted);
    }

    //=====================================================
    // GET ALL COMPANY SALES
    //=====================================================

    public async Task<List<GSal>> GetAllAsync(
        int companyId)
    {
        return await _context.GSales
            .Where(x =>
                x.CompanyId == companyId &&
                x.IsActive &&
                !x.IsDeleted)
            .Include(x => x.Details
                .Where(d => d.IsActive && !d.IsDeleted))
            .OrderByDescending(x => x.docdate)
            .ToListAsync();
    }

    //=====================================================
    // UPDATE
    //=====================================================

    public Task UpdateAsync(GSal sale)
    {
        _context.GSales.Update(sale);
        return Task.CompletedTask;
    }

    //=====================================================
    // SOFT DELETE DETAILS
    //=====================================================

    public Task SoftDeleteDetailsAsync(
        List<GSalDet> details,
        string currentUser)
    {
        foreach (var detail in details)
        {
            detail.IsActive = false;
            detail.IsDeleted = true;

            detail.ModifiedOn = DateTime.UtcNow;
            detail.ModifiedBy = currentUser;

            detail.DeletedOn = DateTime.UtcNow;
            detail.DeletedBy = currentUser;
        }

        return Task.CompletedTask;
    }

    //=====================================================
    // ADD DETAIL
    //=====================================================

    public void AddDetail(GSalDet detail)
    {
        _context.GSaleDetails.Add(detail);
    }

    //=====================================================
    // SAVE CHANGES
    //=====================================================

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}