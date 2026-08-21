using INVENTORYAPP.Data;
using INVENTORYAPP.Infrastructure.Jwt;
using INVENTORYAPP.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace INVENTORYAPP.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddAuthenticationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ===========================
        // Identity
        // ===========================
        services.AddIdentity<
            ApplicationUser,
            ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;

                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        // ===========================
        // JWT Settings
        // ===========================
        services.Configure<JwtSettings>(
            configuration.GetSection("JwtSettings"));

        var jwtSection =
            configuration.GetSection("JwtSettings");

        var secretKey =
            jwtSection["Key"];

        // ===========================
        // Authentication
        // ===========================
        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme =
                    JwtBearerDefaults.AuthenticationScheme;

                options.DefaultChallengeScheme =
                    JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;

                options.SaveToken = true;

                options.TokenValidationParameters =
                    new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "InventoryAPI",

                        ValidAudience = "InventoryClient",

                        IssuerSigningKey =
                            new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(
                                    "ThisIsMySuperSecretKeyForJwtAuthentication2026")),

                        ClockSkew = TimeSpan.Zero
                    };

                //options.Events =
                //    new JwtBearerEvents
                //    {
                //        OnAuthenticationFailed = context =>
                //        {
                //            System.IO.File.AppendAllText(
                //                @"C:\Temp\jwtlog.txt",
                //                DateTime.Now +
                //                Environment.NewLine +
                //                context.Exception +
                //                Environment.NewLine +
                //                "======================" +
                //                Environment.NewLine);

                //            return Task.CompletedTask;
                //        }
                //    };
            });

        return services;
    }
}