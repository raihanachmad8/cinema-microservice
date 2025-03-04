using Swashbuckle.AspNetCore.Annotations;

namespace MovieService.Application.DTOs.Requests
{
    public record MovieRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int DurationInMinutes { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}