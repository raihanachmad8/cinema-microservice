namespace IdentityService.Application.DTOs
{
    public class StudioResponseDto
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public int Capacity { get; set; } 
        public string AdditionalFacilities { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } 
        public DateTime UpdatedAt { get; set; } 
    }
}