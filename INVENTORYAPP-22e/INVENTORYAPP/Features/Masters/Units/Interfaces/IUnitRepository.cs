using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.Units.Interfaces;

public interface IUnitRepository
{
    //==========================================
    // Get All Units Of Company
    //==========================================

    Task<List<Unit>> GetAllAsync(
        int companyId);

    //==========================================
    // Get By Id
    //==========================================

    Task<Unit?> GetByIdAsync(
        int companyId,
        int id);

    //==========================================
    // Get By Code
    //==========================================

    Task<Unit?> GetByCodeAsync(
        int companyId,
        string code);

    //==========================================
    // Exists
    //==========================================

    Task<bool> ExistsAsync(
        int companyId,
        string code);

    //==========================================
    // CRUD
    //==========================================

    Task AddAsync(Unit unit);

    Task UpdateAsync(Unit unit);

    Task DeleteAsync(Unit unit);

    Task SaveChangesAsync();
}