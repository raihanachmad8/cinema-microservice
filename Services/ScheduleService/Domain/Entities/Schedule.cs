using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleService.Domain.Entities
{
    [Table("schedules")]
    public record Schedule : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Guid MovieId { get; set; }

        [Required]
        public Guid StudioId { get; set; } 

        [Required]
        public DateTime ShowTime { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Ticket price must be a positive value.")]
        public decimal TicketPrice { get; set; } 
        
        // Properti navigasi
        public Movie Movie { get; set; } 
        public Studio Studio { get; set; } 
    }
}