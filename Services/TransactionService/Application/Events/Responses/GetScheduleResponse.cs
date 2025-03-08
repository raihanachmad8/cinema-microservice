
namespace TransactionService.Application.Events.Responses;

public class GetScheduleResponse
{
    public int Id { get; set; }
    public int  MovieId { get; set; }
    public int StudioId { get; set; }
    public decimal TicketPrice { get; set; } 
    public DateTime StartDatetime { get; set; }
    public DateTime EndDatetime { get; set; }
}