using IdentityService.Application.DTOs.Requests;
using IdentityService.Application.DTOs.Responses;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Security;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Domain.Enums;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace IdentityService.Application.UseCases
{
    public class LoginHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptoService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly ILogger<LoginHandler> _logger;
        

        public LoginHandler(
            IUserRepository userRepository,
            ITokenService tokenService,
            IConfiguration configuration,
            IJwtService jwtService,
            ICryptographyService cryptoService,
            ILogger<LoginHandler> logger)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
            _configuration = configuration;
            _cryptoService = cryptoService;
            _logger = logger;
        }

        public async Task<AuthResponse> Handle(LoginRequest request)
        {
            _logger.LogInformation("Processing login request for email: {Email}", request.Email);

            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null || !_cryptoService.Verify(user.Password!, request.Password))
            {
                _logger.LogWarning("Login failed: Invalid email or password for email: {Email}", request.Email);
                throw new BadHttpRequestException("Invalid email or password");
            }

            _logger.LogInformation("User {Email} logged in successfully.", user.Email);

            var tokenReponse = await _tokenService.GenerateToken(user);

            return new AuthResponse
            {
                Data = tokenReponse
            };
        }
    }


}