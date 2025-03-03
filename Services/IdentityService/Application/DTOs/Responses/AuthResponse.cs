using System.Text.Json.Serialization;
using IdentityService.Application.Dtos.Responses;

namespace IdentityService.Application.DTOs.Responses
{
    public class AuthResponse : IResponse
    {
        public string Status { get; set; } = "200";
        public string Message { get; set; } = "OK";

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public TokenResponse? Data { get; set; } 
    }
}