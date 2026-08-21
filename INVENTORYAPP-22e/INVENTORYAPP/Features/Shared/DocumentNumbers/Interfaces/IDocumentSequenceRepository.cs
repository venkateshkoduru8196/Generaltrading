using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Shared.DocumentNumbers.Interfaces;

public interface IDocumentSequenceRepository
{
    Task<DocumentSequence?> GetByModuleAsync(
        int companyId,
        string moduleCode);

    Task SaveChangesAsync();
}