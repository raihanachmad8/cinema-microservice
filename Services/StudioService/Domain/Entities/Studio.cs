﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudioService.Domain.Entities;

[Table("studios")]
public record Studio : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [Required] public int Capacity { get; set; }

    public string AdditionalFacilities { get; set; } = string.Empty;
}