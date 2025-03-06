namespace IdentityService.Application.Services;

public class TokenData
{
    public string Id { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; } = DateTime.MinValue;
    public bool IsRevoked { get; set; }
}