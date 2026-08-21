using INVENTORYAPP.Data;
using INVENTORYAPP.Features.Shared.DocumentNumbers.Interfaces;
using INVENTORYAPP.Models;
using Microsoft.EntityFrameworkCore;

namespace INVENTORYAPP.Features.Shared.DocumentNumbers.Repositories;

public class DocumentSequenceRepository : IDocumentSequenceRepository
{
    private readonly AppDbContext _context;

    public DocumentSequenceRepository(AppDbContext context)
    {
        _context = context;
    }




    public async Task<DocumentSequence?> GetByModuleAsync(
    int companyId,
    string moduleCode)
    {
        return await _context.DocumentSequences
            .FirstOrDefaultAsync(x =>
                x.CompanyId == companyId &&
                x.ModuleCode == moduleCode &&
                x.IsActive);
    }








    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
