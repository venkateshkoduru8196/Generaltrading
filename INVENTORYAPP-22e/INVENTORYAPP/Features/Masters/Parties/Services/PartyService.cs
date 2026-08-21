using INVENTORYAPP.Features.Masters.Parties.DTOs;
using INVENTORYAPP.Features.Masters.Parties.Interfaces;
using INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;
using INVENTORYAPP.Models;

namespace INVENTORYAPP.Features.Masters.Parties.Services;

public class PartyService : IPartyService
{
    private readonly IPartyRepository _repository;
    private readonly ICurrentUserService _currentUser;

    public PartyService(
        IPartyRepository repository,
        ICurrentUserService currentUser)
    {
        _repository = repository;
        _currentUser = currentUser;
    }


    public async Task<List<PartyResponse>> GetAllAsync()
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var parties = await _repository.GetAllAsync(companyId);

        return parties
            .Select(MapToResponse)
            .ToList();
    }

    private static PartyResponse MapToResponse(
  Party party)
    {
        return new PartyResponse
        {
            Id = party.Id,
            CompanyId = party.CompanyId,
            PartyCode = party.PartyCode,
            PartyName = party.PartyName,
            IsActive = party.IsActive,
            IsDeleted = party.IsDeleted,
            CreatedOn = party.CreatedOn,
            CreatedBy = party.CreatedBy,
            ModifiedOn = party.ModifiedOn,
            ModifiedBy = party.ModifiedBy,
            DeletedOn = party.DeletedOn,
            DeletedBy = party.DeletedBy
        };
    }

    public async Task<PartyResponse?> GetByIdAsync(int id)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var party = await _repository.GetByIdAsync(
            companyId,
            id);

        if (party == null)
            return null;

        return MapToResponse(party);   
    }


    public async Task<List<PartyLookupResponse>> GetLookupAsync()
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var parties = await _repository.GetAllAsync(companyId);

        return parties
            .Select(x => new PartyLookupResponse
            {
                Id = x.Id,
                PartyCode = x.PartyCode,
                PartyName = x.PartyName
            })
            .ToList();
    }
    public async Task<PartyResponse> CreateAsync(
    CreatePartyRequest request)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        if (await _repository.ExistsAsync(
            companyId,
            request.PartyCode))
        {
            throw new Exception("Party Code already exists.");
        }

        var party = new Party
        {
            CompanyId = companyId,

            PartyCode = request.PartyCode,

            PartyName = request.PartyName,

            IsActive = true,
            IsDeleted = false,

            CreatedOn = DateTime.UtcNow,

            CreatedBy = _currentUser.UserName
        };

        await _repository.AddAsync(party);

        await _repository.SaveChangesAsync();

        return MapToResponse(party);
    }
    public async Task<PartyResponse> UpdateAsync(
    UpdatePartyRequest request)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var party = await _repository.GetByIdAsync(
            companyId,
            request.Id);

        if (party == null)
            throw new Exception("Party not found.");

        var existing = await _repository.GetByCodeAsync(
            companyId,
            request.PartyCode);

        if (existing != null &&
            existing.Id != request.Id)
        {
            throw new Exception("Party Code already exists.");
        }

        party.PartyCode = request.PartyCode;

        party.PartyName = request.PartyName;

        party.ModifiedOn = DateTime.UtcNow;

        party.ModifiedBy = _currentUser.UserName;

        await _repository.UpdateAsync(party);

        await _repository.SaveChangesAsync();

        return MapToResponse(party);
    }
    public async Task DeleteAsync(int id)
    {
        if (!_currentUser.CompanyId.HasValue)
            throw new Exception("Company is not assigned.");

        int companyId = _currentUser.CompanyId.Value;

        var party = await _repository.GetByIdAsync(
            companyId,
            id);

        if (party == null)
            throw new Exception("Party not found.");

        party.DeletedOn = DateTime.UtcNow;

        party.DeletedBy = _currentUser.UserName;

        await _repository.DeleteAsync(party);

        await _repository.SaveChangesAsync();
    }
}