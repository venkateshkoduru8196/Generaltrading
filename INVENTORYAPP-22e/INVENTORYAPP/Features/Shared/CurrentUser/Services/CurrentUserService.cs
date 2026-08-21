using System.Security.Claims;
using INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;
using Microsoft.AspNetCore.Http;

namespace INVENTORYAPP.Features.Shared.CurrentUser.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal? User =>
        _httpContextAccessor.HttpContext?.User;

    public string UserId =>
        User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
        ?? string.Empty;

    public string UserName =>
        User?.FindFirst(ClaimTypes.Name)?.Value
        ?? "SYSTEM";

    public string Email =>
        User?.FindFirst(ClaimTypes.Email)?.Value
        ?? string.Empty;



    public int? CompanyId
    {
        get
        {
            var value = User?.FindFirst("CompanyId")?.Value;

            if (int.TryParse(value, out var companyId))
                return companyId;

            return null;
        }
    }

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated ?? false;
}