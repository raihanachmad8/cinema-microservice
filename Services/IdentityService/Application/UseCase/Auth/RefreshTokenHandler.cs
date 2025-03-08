using IdentityService.Application.DTOs.Responses;
using IdentityService.Application.Interfaces.Services;

namespace IdentityService.Application.UseCase.Auth;

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

    public async Task<Response<TokenResponse?>> Handle(string refreshToken)
    {
        _logger.LogInformation("Processing token refresh for refresh token: {RefreshToken}", refreshToken);

        var tokenReponse = await _tokenService.RefreshToken(refreshToken);

        return new Response<TokenResponse>().Ok(tokenReponse);
    }
}