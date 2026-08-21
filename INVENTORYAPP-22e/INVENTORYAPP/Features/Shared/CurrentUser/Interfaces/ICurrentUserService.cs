using System.Security.Claims;

namespace INVENTORYAPP.Features.Shared.CurrentUser.Interfaces;

public interface ICurrentUserService
{
    string UserId { get; }

    string UserName { get; }

    string Email { get; }

    int? CompanyId { get; }

    bool IsAuthenticated { get; }
}