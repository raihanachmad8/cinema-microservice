using System.Text.Json.Serialization;
using IdentityService.Application.Dtos.Responses;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace IdentityService.Application.DTOs.Responses
{
    public class AuthResponse : IResponse
    {
        public bool Status { get; set; } = true;
        public string Message { get; set; } = "OK";

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public AccessTokenResponse? Data { get; set; } 
    }
}