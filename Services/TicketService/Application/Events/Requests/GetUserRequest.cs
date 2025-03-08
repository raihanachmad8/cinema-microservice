namespace TicketService.Application.Events.Requests;

public class GetUserRequest
{
    public int Id { get; set; }

    public GetUserRequest(int id)
    {
        Id = id;
    }
}