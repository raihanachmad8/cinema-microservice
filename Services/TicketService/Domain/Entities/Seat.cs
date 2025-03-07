using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketService.Domain.Entities
{
    [Table("seats")]
    public record Seat : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int StudioId { get; set; }

        [Required]
        [MaxLength(10)]
        public string SeatNumber { get; set; } = string.Empty;

        [Required]
        public bool IsAvailable { get; set; }

        public DateTime? ReservedAt { get; set; }

        public DateTime? OccupiedAt { get; set; }
    }
}