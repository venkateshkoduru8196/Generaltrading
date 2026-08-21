using FluentValidation;
using INVENTORYAPP.Features.Masters.Accounts.DTOs;

namespace INVENTORYAPP.Features.Masters.Accounts.Validators;

public class CreateAccountRequestValidator
    : AbstractValidator<CreateAccountRequest>
{
    public CreateAccountRequestValidator()
    {
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