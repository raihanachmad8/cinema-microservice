namespace StudioService.Application.Events.Responses;

public class GetStudioResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; }
    public string AdditionalFacilities { get; set; }
}