using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieService.Domain.Entities;

public record class BaseEntity
{
    [Required] [Column("created_at")] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Required] [Column("updated_at")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [Column("deleted_at")] public DateTime? DeletedAt { get; set; }
}