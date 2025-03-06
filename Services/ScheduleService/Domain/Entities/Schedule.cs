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
        public int MovieId { get; set; }

        [Required]
        public int StudioId { get; set; } 

        [Required]
        public DateTime StartDatetime { get; set; }
        public DateTime EndDatetime { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Ticket price must be a positive value.")]
        public decimal TicketPrice { get; set; }
    }
}