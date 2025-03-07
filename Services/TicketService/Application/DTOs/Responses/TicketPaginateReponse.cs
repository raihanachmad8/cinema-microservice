using TicketService.Domain.Entities;

namespace TicketService.Application.DTOs.Responses;

public record TicketPaginateResponse
{
    public IEnumerable<Ticket> Tickets { get; set; } = new List<Ticket>();
    public Metadata? Metadata { get; set; }
}