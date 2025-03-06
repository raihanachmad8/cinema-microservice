using IdentityService.Application.DTOs.Responses;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Security;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Domain.Enums;
using Microsoft.AspNetCore.Authentication.BearerToken;
using System.Security.Claims;
using IdentityService.Application.DTOs.Responses;

namespace IdentityService.Application.UseCases;

public class RefreshTokenHandler
{
    private readonly ITokenService _tokenService;
    private readonly ISerilog<RefreshTokenHandler> _logger;

    public RefreshTokenHandler(
        ITokenService tokenService,
        ISerilog<RefreshTokenHandler> logger)
    {
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<Response<TokenResponse>> Handle(string refreshToken)
    {
        _logger.LogInformation("Processing token refresh for refresh token: {RefreshToken}", refreshToken);

        var tokenReponse = await _tokenService.RefreshToken(refreshToken);

        return new Response<TokenResponse>().Ok(tokenReponse!);
    }
}