namespace TransactionService.Application.Events.Requests;

public class GetTransactionRequest
{
    public int Id { get; set; }

    public GetTransactionRequest(int id)
    {
        Id = id;
    }
}