using FluentValidation;
using MovieService.Domain.Enums;

public class GenreRouteValidator : AbstractValidator<string>
{
    public GenreRouteValidator()
    {
        RuleFor(x => x)
            .NotEmpty().WithMessage("Genre is required.")
            .Matches("^[a-zA-Z]+$").WithMessage("Genre must contain only letters.")
            .Must(BeAValidGenre).WithMessage("Invalid genre provided.");
    }

    private bool BeAValidGenre(string genre)
    {
        return Enum.TryParse<Genre>(genre, true, out _);
    }
}