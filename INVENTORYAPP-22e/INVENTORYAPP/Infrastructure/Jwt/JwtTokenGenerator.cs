using INVENTORYAPP.Models;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace INVENTORYAPP.Infrastructure.Jwt;

public class JwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    public JwtTokenGenerator(
        IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(
        ApplicationUser user,
        IList<string> roles)
    {
        var claims = new List<Claim>
        {
            // User Id
            new Claim(
                ClaimTypes.NameIdentifier,
                user.Id),

            // User Name
            new Claim(
                ClaimTypes.Name,
                user.UserName ?? string.Empty),

            // Email
            new Claim(
                ClaimTypes.Email,
                user.Email ?? string.Empty),

            // Company (Multi-Tenant)
            new Claim(
                "CompanyId",
                user.CompanyId?.ToString() ?? string.Empty)
        };

        // Roles
        foreach (var role in roles)
        {
            claims.Add(
                new Claim(
                    ClaimTypes.Role,
                    role));
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                _jwtSettings.AccessTokenExpiryMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}