using CinemaApp.Domain.Enums;
using IdentityService.Application.DTOs.Requests;
using IdentityService.Application.DTOs.Responses;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Security;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Common.Exceptions;
using IdentityService.Domain.Entities;
using MovieService.Application.DTOs.Responses;

namespace IdentityService.Application.UseCases;

public class RegisterHandler
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    private readonly ICryptographyService _cryptoService;
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;
    private readonly ILogger<RegisterHandler> _logger;

    public RegisterHandler(
        IUserRepository userRepository,
        ITokenService tokenService,
        IConfiguration configuration,
        IJwtService jwtService,
        ICryptographyService cryptoService,
        ILogger<RegisterHandler> logger)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _configuration = configuration;
        _jwtService = jwtService;
        _cryptoService = cryptoService;
        _logger = logger;
    }

    public async Task<Response<TokenResponse>> Handle(RegisterRequest request)
    {
        _logger.LogInformation("Processing registration request for email: {Email}", request.Email);

        var existingUser = await _userRepository.GetByEmailAsync(request.Email);
        if (existingUser != null)
        {
            _logger.LogWarning("Registration failed: Email {Email} already exists", request.Email);
            throw new ConflictException("User with email already exists");
        }

        var hashedPassword = _cryptoService.Hash(request.Password);

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Password = hashedPassword,
            CreatedAt = DateTime.UtcNow,
            Role = Role.User
        };

        await _userRepository.AddAsync(user);
        _logger.LogInformation("User {Email} registered successfully.", user.Email);

        var tokenResponse = await _tokenService.GenerateToken(user);
        return new Response<TokenResponse>().Created(tokenResponse!);
    }
}