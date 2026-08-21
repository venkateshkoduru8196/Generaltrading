using INVENTORYAPP.Features.Masters.Units.DTOs;

namespace INVENTORYAPP.Features.Masters.Units.Interfaces;

public interface IUnitService
{
    //==========================================
    // Get All Units
    //==========================================

    Task<List<UnitResponse>> GetAllAsync();

    //==========================================
    // Get By Id
    //==========================================

    Task<UnitResponse?> GetByIdAsync(
        int id);

    //==========================================
    // Lookup
    //==========================================

    Task<List<UnitLookupResponse>> GetLookupAsync();

    //==========================================
    // Create
    //==========================================

    Task CreateAsync(
        CreateUnitRequest request);

    //==========================================
    // Update
    //==========================================

    Task UpdateAsync(
        UpdateUnitRequest request);

    //==========================================
    // Delete
    //==========================================

    Task DeleteAsync(
        int id);
}