using INVENTORYAPP.Features.Masters.Parties.DTOs;

namespace INVENTORYAPP.Features.Masters.Parties.Interfaces;

public interface IPartyService
{
    Task<List<PartyResponse>> GetAllAsync();

    Task<PartyResponse?> GetByIdAsync(int id);

    Task<List<PartyLookupResponse>> GetLookupAsync();

    Task<PartyResponse> CreateAsync(
        CreatePartyRequest request);

    Task<PartyResponse> UpdateAsync(
        UpdatePartyRequest request);

    Task DeleteAsync(int id);
}