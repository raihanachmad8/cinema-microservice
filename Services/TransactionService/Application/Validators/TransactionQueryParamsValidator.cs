using FluentValidation;
using TransactionService.Application.DTOs.Requests;

namespace TransactionService.Application.Validators
{
    public class TransactionQueryParamsValidator : AbstractValidator<TransactionQueryParams>
    {
        public TransactionQueryParamsValidator()
        {
            RuleFor(x => x.PaymentStatus)
                .Must(status => status == null || status.Count > 0)
                .WithMessage("At least one status must be provided if Payment Status is specified.");

            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page number must be greater than 0.");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).WithMessage("Page size must be greater than 0.")
                .LessThanOrEqualTo(100).WithMessage("Page size must not exceed 100.");

            RuleFor(x => x.Sort)
                .Must(sort => string.IsNullOrEmpty(sort) || sort.ToLower() == "asc" || sort.ToLower() == "desc")
                .WithMessage("Sort must be either 'asc' or 'desc'.");

            RuleFor(x => x.OrderBy)
                .Must(orderBy => string.IsNullOrEmpty(orderBy) || IsValidColumn(orderBy))
                .WithMessage("OrderBy must be a valid column name.");
        }

        private bool IsValidColumn(string columnName)
        {
            var validColumns = new List<string> { "TicketId", "UserId", "PaymentMethod", "PaymentStatus", "TransactionDate", "TotalAmount" };
            return validColumns.Contains(columnName);
        }
    }
}