using System.ComponentModel.DataAnnotations;
using MovieService.Domain.Enums;

namespace MovieService.Application.DTOs.Requests
{
    public record MovieRequest
    {
        public string Title { get; set; } = string.Empty;
        public Genre Genre { get; set; }
        public int DurationInMinutes { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}