using FluentValidation;
using INVENTORYAPP.Features.Masters.Parties.DTOs;

namespace INVENTORYAPP.Features.Masters.Parties.Validators;

public class UpdatePartyRequestValidator
    : AbstractValidator<UpdatePartyRequest>
{
    public UpdatePartyRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Invalid Party Id.");

        RuleFor(x => x.PartyCode)
            .NotEmpty()
            .WithMessage("Party Code is required.")
            .MaximumLength(20)
            .WithMessage("Party Code cannot exceed 20 characters.");

        RuleFor(x => x.PartyName)
            .NotEmpty()
            .WithMessage("Party Name is required.")
            .MaximumLength(150)
            .WithMessage("Party Name cannot exceed 150 characters.");
    }
}