using FluentValidation;
using TransactionService.Application.DTOs.Requests;

namespace TransactionService.Application.Validators;

public class TransactionPaymentRequestValidator : AbstractValidator<TransactionPaymentRequest>
{
    public TransactionPaymentRequestValidator()
    {

        RuleFor(x => x.Amount)
            .GreaterThan(0).WithMessage("Amount must be greater than 0.");
    }
}