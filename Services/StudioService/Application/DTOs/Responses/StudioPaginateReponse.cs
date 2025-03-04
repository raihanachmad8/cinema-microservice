using StudioService.Domain.Entities;

namespace StudioService.Application.DTOs.Responses
{
    public record StudioPaginateResponse
    {
        public IEnumerable<Studio> Studios { get; set; } = new List<Studio>();
        public Metadata Metadata { get; set; }
    }
}