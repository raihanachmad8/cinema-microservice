using FluentValidation;
using IdentityService.Application.DTOs.Requests;

namespace IdentityService.Application.Validators;

public class UserRegisterValidator : AbstractValidator<RegisterRequest>
{
    public UserRegisterValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(3, 50).WithMessage("Name must be between 3 and 50 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Matches(@"(?=.*[a-z])").WithMessage("Password must contain at least one lowercase letter.")
            .WithMessage("Password must be at least 8 characters long.")
            .Matches(@"(?=.*[!@#$%^&*()_+\-=\[\]{};':""|,.<>\/?])")
            .WithMessage("Password must contain at least one symbol.")
            .Length(6, 50).WithMessage("Password must be between 6 and 50 characters.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password is required.")
            .Equal(x => x.Password).WithMessage("Password and confirm password must match.");
    }
}