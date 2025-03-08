namespace ScheduleService.Application.Events.Requests;

public class GetScheduleRequest
{
    public int Id { get; set; }

    public GetScheduleRequest(int id)
    {
        Id = id;
    }
}