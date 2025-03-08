using TransactionService.Domain.Enums;

namespace TransactionService.Application.DTOs.Requests
{
    public record TransactionRequest
    {
        public int TicketId { get; set; }
        public string PaymentMethod { get; set; }
    }
}