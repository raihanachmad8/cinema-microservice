using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TransactionService.Domain.Enums;

namespace TransactionService.Domain.Entities
{
    [Table("transactions")]
    public record Transaction : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TicketId { get; set; } 

        [Required]
        public int UserId { get; set; }  

        [Required]
        [MaxLength(50)]
        public PaymentMethod PaymentMethod { get; set; }  
        
        [Required]
        [MaxLength(20)]
        public PaymentStatus PaymentStatus { get; set; }  

        [Required]
        public DateTime TransactionDate { get; set; } 

        [Required]
        public decimal TotalAmount { get; set; }  
    }
}