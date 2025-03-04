using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MovieService.Domain.Enums;

namespace MovieService.Domain.Entities 
{
    [Table("movies")]
    public record  Movie: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required, MaxLength(255)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public Genre Genre { get; set; }

        [Required]
        [Range(1, 300, ErrorMessage = "Duration must be between 1 and 300 minutes.")]
        public int DurationInMinutes { get; set; }  

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}