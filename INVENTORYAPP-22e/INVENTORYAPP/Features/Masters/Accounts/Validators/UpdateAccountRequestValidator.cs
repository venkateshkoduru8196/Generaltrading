using FluentValidation;
using INVENTORYAPP.Features.Masters.Accounts.DTOs;

namespace INVENTORYAPP.Features.Masters.Accounts.Validators;

public class UpdateAccountRequestValidator
    : AbstractValidator<UpdateAccountRequest>
{
    public UpdateAccountRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Invalid Account Id.");

        RuleFor(x => x.AccountCode)
            .NotEmpty()
            .WithMessage("Account Code is required.")
            .MaximumLength(20)
            .WithMessage("Account Code cannot exceed 20 characters.");

        RuleFor(x => x.AccountName)
            .NotEmpty()
            .WithMessage("Account Name is required.")
            .MaximumLength(150)
            .WithMessage("Account Name cannot exceed 150 characters.");
    }
}

