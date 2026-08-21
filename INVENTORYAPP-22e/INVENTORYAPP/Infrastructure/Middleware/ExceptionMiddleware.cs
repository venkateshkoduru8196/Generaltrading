using System.Text.Json;
using INVENTORYAPP.Infrastructure.Exceptions;

namespace INVENTORYAPP.Infrastructure.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(
        RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context)
    {
        try
        {
            await _next(context);
        }

        catch (AppException ex)
        {
            context.Response.StatusCode =
                StatusCodes.Status400BadRequest;

            context.Response.ContentType =
                "application/json";

            var response = new
            {
                Success = false,
                Message = ex.Message
            };

            await context.Response.WriteAsync(

                JsonSerializer.Serialize(response)

            );
        }

        catch (Exception ex)
        {
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            context.Response.ContentType =
                "application/json";

            var response = new
            {
                Success = false,
                Message = ex.Message,
                Exception = ex.ToString()
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response)
            );
        }
    }
}