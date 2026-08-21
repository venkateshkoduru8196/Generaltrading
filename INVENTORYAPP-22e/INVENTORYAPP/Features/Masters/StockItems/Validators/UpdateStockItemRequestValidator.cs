using FluentValidation;
using INVENTORYAPP.Features.Masters.StockItems.DTOs;

namespace INVENTORYAPP.Features.Masters.StockItems.Validators;

public class UpdateStockItemRequestValidator
    : AbstractValidator<UpdateStockItemRequest>
{
    public UpdateStockItemRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Invalid Stock Item Id.");

        RuleFor(x => x.StockCode)
            .NotEmpty()
            .WithMessage("Stock Code is required.")
            .MaximumLength(20)
            .WithMessage("Stock Code cannot exceed 20 characters.");

        RuleFor(x => x.StockName)
            .NotEmpty()
            .WithMessage("Stock Name is required.")
            .MaximumLength(150)
            .WithMessage("Stock Name cannot exceed 150 characters.");

        RuleFor(x => x.TaxRate)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Tax Rate cannot be negative.");
    }
}