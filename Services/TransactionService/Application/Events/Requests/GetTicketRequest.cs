namespace TransactionService.Application.Events.Requests;

public class GetTicketRequest
{
    public int Id { get; set; }

    public GetTicketRequest(int id)
    {
        Id = id;
    }
}