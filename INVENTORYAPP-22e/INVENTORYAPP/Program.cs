using INVENTORYAPP.Extensions;
using INVENTORYAPP.Features.Masters.Accounts.Validators;
using INVENTORYAPP.Infrastructure.Middleware;
using Microsoft.IdentityModel.Logging;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using INVENTORYAPP.Models;

var builder = WebApplication.CreateBuilder(args);

// ======================================================
// Controllers
// ======================================================

builder.Services.AddControllers();

// ======================================================
// Database
// ======================================================

builder.Services.AddDatabase(builder.Configuration);

// ======================================================
// Authentication + JWT
// ======================================================

builder.Services.AddAuthenticationServices(builder.Configuration);

// ======================================================
// Fluent Validation
// ======================================================

builder.Services.AddValidatorsFromAssemblyContaining<CreateAccountRequestValidator>();

// ======================================================
// Dependency Injection
// ======================================================

builder.Services.AddDependencyInjection();

// ======================================================
// CORS
// ======================================================

builder.Services.AddCorsPolicy();

// ======================================================
// Swagger
// ======================================================

builder.Services.AddSwaggerDocumentation();

// ======================================================
// Show Identity Errors (Development)
// ======================================================

IdentityModelEventSource.ShowPII = true;

// ======================================================
// Build Application
// ======================================================

var app = builder.Build();

// ======================================================
// Global Exception Middleware
// ======================================================

app.UseMiddleware<ExceptionMiddleware>();

// ======================================================
// Seed Database
// ======================================================

await app.SeedDatabaseAsync();

// ======================================================
// Swagger
// ======================================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ======================================================
// Middleware Pipeline
// ======================================================
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();

app.UseCors("ReactPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();