using TransactionService.Application.DTOs.Responses;
using TransactionService.Domain.Enums;

namespace TransactionService.Application.UseCases
{
    public class GetPaymentHandler
    {
        public GetPaymentHandler()
        {
        }

        public Task<Response<List<string>>> Handle()
        {
            var paymentMethod = Enum.GetValues(typeof(PaymentMethod))
                .Cast<PaymentMethod>()
                .Select(g => g.ToString())
                .ToList();

            return Task.FromResult(new Response<List<string>>().Ok(paymentMethod, "Payment Method"));
        }
    }
}