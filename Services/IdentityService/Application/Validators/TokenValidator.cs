using FluentValidation;

public class TokenValidator : AbstractValidator<string>
{
    public TokenValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Authorization header must not be empty.")
            .Matches(@"^Bearer\s[A-Za-z0-9\-_.]+$")
            .WithMessage("Authorization header must start with 'Bearer ' and contain a valid token.");
    }
}