using FluentValidation;
using StudioService.Application.DTOs.Requests;

namespace IdentityService.Application.Validators
{
    public class StudioQueryParamsValidator : AbstractValidator<StudioQueryParams>
    {
        public StudioQueryParamsValidator()
        {
            RuleFor(x => x.Search)
                .MaximumLength(100).WithMessage("Search term must not exceed 100 characters.");

            RuleFor(x => x.Columns)
                .Must(columns => columns == null || columns.Count <= 10)
                .WithMessage("You can specify a maximum of 10 columns.");

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
            // Here you can implement logic to check if the column name is valid.
            // For example, you can check against a list of valid column names.
            var validColumns = new List<string> { "Name", "Capacity", "AdditionalFacilities", "CreatedAt", "UpdatedAt" };
            return validColumns.Contains(columnName);
        }
    }
}