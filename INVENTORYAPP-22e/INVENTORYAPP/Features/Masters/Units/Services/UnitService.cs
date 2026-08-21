using INVENTORYAPP.Features.Masters.Units.DTOs;
using INVENTORYAPP.Features.Masters.Units.Interfaces;
using INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;
using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.Units.Services;

public class UnitService : IUnitService
{
    private readonly IUnitRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public UnitService(
        IUnitRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }

    //==========================================
    // Get All
    //==========================================

    public async Task<List<UnitResponse>> GetAllAsync()
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found.");

        var units = await _repository.GetAllAsync(
            _currentUser.CompanyId.Value);

        return units
            .Select(MapToResponse)
            .ToList();
    }

    //==========================================
    // Get By Id
    //==========================================

    public async Task<UnitResponse?> GetByIdAsync(
        int id)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found.");

        var unit =
            await _repository.GetByIdAsync(
                _currentUser.CompanyId.Value,
                id);

        if (unit == null)
            return null;

        return MapToResponse(unit);
    }

    //==========================================
    // Lookup
    //==========================================

    public async Task<List<UnitLookupResponse>> GetLookupAsync()
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found.");

        var units =
            await _repository.GetAllAsync(
                _currentUser.CompanyId.Value);

        return units
            .Select(x => new UnitLookupResponse
            {
                Id = x.Id,
                code = x.code,
                description = x.description
            })
            .ToList();
    }

    //==========================================
    // Create
    //==========================================

    public async Task CreateAsync(
        CreateUnitRequest request)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found.");

        int companyId =
            _currentUser.CompanyId.Value;

        if (await _repository.ExistsAsync(
            companyId,
            request.code))
        {
            throw new Exception(
                "Unit code already exists.");
        }

        var unit = new Unit
        {
            CompanyId = companyId,

            code = request.code,

            description = request.description,

            IsActive = true,

            IsDeleted = false,

            CreatedOn = DateTime.UtcNow,

            CreatedBy = _currentUser.UserName
        };

        await _repository.AddAsync(unit);

        await _repository.SaveChangesAsync();
    }


    //==========================================
    // Update
    //==========================================

    public async Task UpdateAsync(
        UpdateUnitRequest request)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found.");

        int companyId =
            _currentUser.CompanyId.Value;

        var unit =
            await _repository.GetByIdAsync(
                companyId,
                request.Id);

        if (unit == null)
            throw new Exception("Unit not found.");

        var existing =
            await _repository.GetByCodeAsync(
                companyId,
                request.code);

        if (existing != null &&
            existing.Id != request.Id)
        {
            throw new Exception(
                "Unit code already exists.");
        }

        unit.code = request.code;

        unit.description = request.description;

        unit.ModifiedOn = DateTime.UtcNow;

        unit.ModifiedBy = _currentUser.UserName;

        await _repository.UpdateAsync(unit);

        await _repository.SaveChangesAsync();
    }

    //==========================================
    // Delete
    //==========================================

    public async Task DeleteAsync(
        int id)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company not found.");

        int companyId =
            _currentUser.CompanyId.Value;

        var unit =
            await _repository.GetByIdAsync(
                companyId,
                id);

        if (unit == null)
            throw new Exception("Unit not found.");

        unit.DeletedOn = DateTime.UtcNow;

        unit.DeletedBy = _currentUser.UserName;

        await _repository.DeleteAsync(unit);

        await _repository.SaveChangesAsync();
    }

    //==========================================
    // Map Entity To Response
    //==========================================

    private static UnitResponse MapToResponse(
        Unit unit)
    {
        return new UnitResponse
        {
            Id = unit.Id,

            CompanyId = unit.CompanyId,

            code = unit.code,

            description = unit.description,

            IsActive = unit.IsActive,

            IsDeleted = unit.IsDeleted,

            CreatedOn = unit.CreatedOn,

            CreatedBy = unit.CreatedBy,

            ModifiedOn = unit.ModifiedOn,

            ModifiedBy = unit.ModifiedBy,

            DeletedOn = unit.DeletedOn,

            DeletedBy = unit.DeletedBy
        };
    }
}


