namespace TransactionService.Application.Events.Requests;

public class GetStudioRequest
{
    public int Id { get; set; }

    public GetStudioRequest(int id)
    {
        Id = id;
    }
}