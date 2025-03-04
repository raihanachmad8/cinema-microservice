using System.ComponentModel.DataAnnotations;

namespace StudioService.Application.DTOs.Requests;

public record StudioRequest
{
    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [Required] public int Capacity { get; set; }

    public string AdditionalFacilities { get; set; } = string.Empty;
}