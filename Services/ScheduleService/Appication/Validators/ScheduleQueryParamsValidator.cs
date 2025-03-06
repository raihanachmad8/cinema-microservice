using FluentValidation;
using ScheduleService.Application.DTOs.Requests;

namespace ScheduleService.Application.Validators
{
    public class ScheduleQueryParamsValidator : AbstractValidator<ScheduleQueryParams>
    {
        public ScheduleQueryParamsValidator()
        {
            RuleFor(x => x.MovieId)
                .Must(BeAValidGuid).WithMessage("MovieId must be a valid GUID.")
                .When(x => !string.IsNullOrEmpty(x.MovieId)); 

            RuleFor(x => x.StudioId)
                .Must(BeAValidGuid).WithMessage("StudioId must be a valid GUID.")
                .When(x => !string.IsNullOrEmpty(x.StudioId));

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

        private bool BeAValidGuid(string? guid)
        {
            return string.IsNullOrEmpty(guid) || Guid.TryParse(guid, out _);
        }

        private bool IsValidColumn(string columnName)
        {
            var validColumns = new List<string> { "MovieId", "StudioId", "StartDatetime", "EndDatetime", "TicketPrice", "CreatedAt", "UpdatedAt" };
            return validColumns.Contains(columnName);
        }
    }
}