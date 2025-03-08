namespace TicketService.Application.Events.Requests;

public class GetMovieRequest
{
    public int Id { get; set; }

    public GetMovieRequest(int id)
    {
        Id = id;
    }
}