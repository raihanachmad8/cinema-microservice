using FluentValidation;
using StudioService.Application.DTOs.Requests;

namespace StudioService.Application.Validators;

public class StudioRequestValidator : AbstractValidator<StudioRequest>
{
    public StudioRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Studio Name is required.")
            .MaximumLength(100).WithMessage("Studio Name must not exceed 100 characters.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0).WithMessage("Capacity must be greater than 0.");

        RuleFor(x => x.AdditionalFacilities)
            .MaximumLength(500).WithMessage("Additional facilities must not exceed 500 characters.");
    }
}