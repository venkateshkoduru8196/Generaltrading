using Microsoft.Extensions.DependencyInjection;

namespace INVENTORYAPP.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicy(
        this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("ReactPolicy", policy =>
            {
                policy
                    .WithOrigins(
                        "http://localhost:5173",
                        "http://localhost:5174",
                        "https://general-trading.genbasesoftware.com")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}