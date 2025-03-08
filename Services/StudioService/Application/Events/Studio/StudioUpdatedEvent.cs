namespace StudioService.Appication.Events.User;

public class StudioUpdatedEvent
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string AdditionalFacilities { get; set; } = string.Empty;
    
}