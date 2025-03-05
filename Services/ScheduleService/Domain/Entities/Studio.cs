using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScheduleService.Domain.Entities;

[Table("studios")]
public record Studio : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [Required] public int Capacity { get; set; }

    public string AdditionalFacilities { get; set; } = string.Empty;
}