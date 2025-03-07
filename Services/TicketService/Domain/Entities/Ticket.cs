using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TicketService.Domain.Enums;

namespace TicketService.Domain.Entities
{
    [Table("tickets")]
    public record Ticket : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ScheduleId { get; set; }
        
        [Required]
        public int UserId { get; set; }

        [Required]
        public int SeatId { get; set; }

        [Required]
        public TicketStatus Status { get; set; }
        
        public DateTime? ReservedAt { get; set; }
        
        [ForeignKey("SeatId")]
        public virtual Seat Seat { get; set; }
    }
}