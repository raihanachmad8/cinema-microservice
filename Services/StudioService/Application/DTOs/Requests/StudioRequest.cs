using System.ComponentModel.DataAnnotations;

namespace IdentityService.Application.DTOs
{
    public class StudioRequestDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; 

        [Required]
        public int Capacity { get; set; }  

        public string AdditionalFacilities { get; set; } = string.Empty; 
    }
}