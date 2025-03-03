using FluentValidation;
using IdentityService.Application.DTOs.Requests;

namespace IdentityService.Application.Validators
{
    public class UserLoginValidator : AbstractValidator<LoginRequest>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}
