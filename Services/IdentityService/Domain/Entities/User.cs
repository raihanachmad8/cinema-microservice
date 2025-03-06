using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CinemaApp.Domain.Enums;
using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Entities;

[Table("users")]
public class User : BaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required] [MaxLength(100)] public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;

    [Required] public Role Role { get; set; } = Role.User;

    [Required] [MaxLength(15)] public string PhoneNumber { get; set; } = string.Empty;

    [Required] [MaxLength(255)] public string Address { get; set; } = string.Empty;
}