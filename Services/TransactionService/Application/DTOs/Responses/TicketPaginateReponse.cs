using TransactionService.Domain.Entities;

namespace TransactionService.Application.DTOs.Responses;

public record TicketPaginateResponse
{
    public IEnumerable<Transaction> Tickets { get; set; } = new List<Transaction>();
    public Metadata? Metadata { get; set; }
}