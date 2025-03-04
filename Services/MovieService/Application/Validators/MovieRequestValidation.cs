using FluentValidation;
using MovieService.Application.DTOs.Requests;

namespace MovieService.Application.Validators
{
    public class MovieRequestValidator : AbstractValidator<MovieRequest>
    {
        public MovieRequestValidator()
        {
            RuleFor(movie => movie.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

            RuleFor(movie => movie.Genre)
                .IsInEnum().WithMessage("Genre is required.");

            RuleFor(movie => movie.DurationInMinutes)
                .NotEmpty().WithMessage("Duration in minutes is required.")
                .InclusiveBetween(1, 300).WithMessage("Duration must be between 1 and 300 minutes.");

            RuleFor(movie => movie.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}