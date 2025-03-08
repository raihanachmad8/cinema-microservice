using FluentValidation;
using TransactionService.Application.DTOs.Requests;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.Validators
{
    public class TransactionRequestValidator : AbstractValidator<TransactionRequest>
    {
        public TransactionRequestValidator()
        {
            RuleFor(x => x.TicketId)
                .GreaterThan(0).WithMessage("TicketId must be greater than 0.");
            RuleFor(x => x.PaymentMethod)
                .NotEmpty().WithMessage("Payment is required.")
                .Must(BeAValidGenre).WithMessage("Invalid payment provided.");
        }
        
        private bool BeAValidGenre(string payment)
        {
            return Enum.TryParse<PaymentMethod>(payment, true, out _);
        }
    }
}