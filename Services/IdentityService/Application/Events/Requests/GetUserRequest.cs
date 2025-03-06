namespace IdentityService.Application.Events.Requests;

public class GetUserRequest
{
    public int Id { get; set; }

    public GetUserRequest(int userId)
    {
        Id = userId;
    }
}