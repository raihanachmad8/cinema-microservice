namespace IdentityService.Application.Services
{
    public class TokenData
    {
        public string Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public bool IsRevoked { get; set; }
    }
}