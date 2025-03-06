namespace StudioService.Appication.Events.User;

public class StudioCreatedEvent
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string AdditionalFacilities { get; set; } = string.Empty;
}