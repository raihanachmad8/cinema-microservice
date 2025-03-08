using FluentValidation;
using ScheduleService.Application.DTOs.Requests;

namespace ScheduleService.Application.Validators
{
    public class ScheduleQueryParamsValidator : AbstractValidator<ScheduleQueryParams>
    {
        public ScheduleQueryParamsValidator()
        {
            RuleFor(x => x.MovieId)
                .Must(BeAValidPositiveInteger).WithMessage("MovieId must be a positive integer.")
                .When(x => x.MovieId.HasValue); // Validate only if MovieId is provided

            RuleFor(x => x.StudioId)
                .Must(BeAValidPositiveInteger).WithMessage("StudioId must be a positive integer.")
                .When(x => x.StudioId.HasValue); // Validate only if StudioId is provided

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

        private bool BeAValidPositiveInteger(int? value)
        {
            return !value.HasValue || (value > 0);
        }

        private bool IsValidColumn(string columnName)
        {
            var validColumns = new List<string> { "MovieId", "StudioId", "StartDatetime", "EndDatetime", "TicketPrice", "CreatedAt", "UpdatedAt" };
            return validColumns.Contains(columnName);
        }
    }
}