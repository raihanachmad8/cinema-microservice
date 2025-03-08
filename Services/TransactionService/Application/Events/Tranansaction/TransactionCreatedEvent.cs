using TransactionService.Application.DTOs.Responses;
using TransactionService.Domain.Enums;

namespace TransactionService.Appication.Events.User;

public class TransactionCreatedEvent
{
    public int Id { get; set; }

    public int TicketId { get; set; }

    public int UserId { get; set; }

    public PaymentMethod PaymentMethod { get; set; }

    public PaymentStatus PaymentStatus { get; set; }

    public DateTime TransactionDate { get; set; }

    public decimal TotalAmount { get; set; }
}