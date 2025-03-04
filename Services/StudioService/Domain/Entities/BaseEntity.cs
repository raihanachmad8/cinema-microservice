using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioService.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Required, Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required, Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }  
    }
}
