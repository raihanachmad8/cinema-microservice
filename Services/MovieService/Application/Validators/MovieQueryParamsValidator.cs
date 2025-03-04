using FluentValidation;
using MovieService.Application.DTOs.Requests;

namespace MovieService.Application.Validators;

public class MovieQueryParamsValidator : AbstractValidator<MovieQueryParams>
{
    public MovieQueryParamsValidator()
    {
        RuleFor(x => x.Search)
            .MaximumLength(100).WithMessage("Search term must not exceed 100 characters.");

        RuleFor(x => x.OrderBy)
            .Must(orderBy => string.IsNullOrEmpty(orderBy) || IsValidColumn(orderBy))
            .WithMessage("OrderBy must be a valid column name.");

        RuleFor(x => x.Sort)
            .Must(sort => string.IsNullOrEmpty(sort) || sort.ToLower() == "asc" || sort.ToLower() == "desc")
            .WithMessage("Sort must be either 'asc' or 'desc'.");

        RuleFor(x => x.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100.");
    }

    private bool IsValidColumn(string columnName)
    {
        var validColumns = new List<string> { "Title", "Genre", "DurationInMinutes", "Description", "CreatedAt", "UpdatedAt" };
        return validColumns.Contains(columnName);
    }
}