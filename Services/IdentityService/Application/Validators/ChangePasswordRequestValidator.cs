using FluentValidation;
using IdentityService.Application.DTOs.Requests;

namespace IdentityService.Application.Validators
{
    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password cannot be empty.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password cannot be empty.")
                .MinimumLength(8).WithMessage("New password must be at least 8 characters.")
                .Matches(@"[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("New password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("New password must contain at least one number.")
                .Matches(@"[\W_]").WithMessage("New password must contain at least one special character (e.g. @, #, $, %, etc).");

            RuleFor(x => x.ConfirmNewPassword)
                .NotEmpty().WithMessage("Confirm new password cannot be empty.")
                .Equal(x => x.NewPassword).WithMessage("New password and confirm password must match.");
        }
    }
}