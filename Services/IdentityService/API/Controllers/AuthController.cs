using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using IdentityService.Application.DTOs.Requests;
using IdentityService.Application.UseCase.Auth;
using IdentityService.Application.UseCases;
using Microsoft.AspNetCore.Authorization;

namespace IdentityService.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IValidator<RegisterRequest> _registerValidator;
    private readonly IValidator<LoginRequest> _loginValidator;
    private readonly IValidator<string> _tokenValidator;
    private readonly LoginHandler _loginHandler;
    private readonly RegisterHandler _registerHandler;
    private readonly RefreshTokenHandler _refreshTokenHandler;
    private readonly LogoutHandler _logoutHandler;

    public AuthController(
        IValidator<RegisterRequest> registerValidator,
        IValidator<LoginRequest> loginValidator,
        IValidator<string> tokenValidator,
        RegisterHandler registerHandler,
        LoginHandler loginHandler,
        RefreshTokenHandler refreshTokenHandler,
        LogoutHandler logoutHandler
    )
    {
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _tokenValidator = tokenValidator;
        _registerHandler = registerHandler;
        _loginHandler = loginHandler;
        _refreshTokenHandler = refreshTokenHandler;
        _logoutHandler = logoutHandler;
    }


    [HttpPost]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        await _registerValidator.ValidateAsync(request);

        var result = await _registerHandler.Handle(request);
        return CreatedAtAction(nameof(Register), result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        await _loginValidator.ValidateAsync(request);

        var result = await _loginHandler.Handle(request);
        return Ok(result);
    }

    [HttpPost("refresh")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> Refresh([FromHeader(Name = "Authorization")] string authorization)
    {
        await _tokenValidator.ValidateAsync(authorization);

        var result = await _refreshTokenHandler.Handle(authorization.Split(" ")[1]);
        return Ok(result);
    }

    [HttpDelete("logout")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> Logout([FromHeader(Name = "Authorization")] string authorization)
    {
        await _tokenValidator.ValidateAsync(authorization);

        await _logoutHandler.Handle(authorization.Split(" ")[1]);
        return NoContent();
    }
}