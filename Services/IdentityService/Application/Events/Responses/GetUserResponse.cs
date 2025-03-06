using CinemaApp.Domain.Enums;

namespace IdentityService.Application.Events.Responses;

public class GetUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}