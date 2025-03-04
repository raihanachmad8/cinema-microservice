using System.Security.Claims;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Security;
using IdentityService.Application.Interfaces.Services;
using IdentityService.Domain.Enums;

namespace IdentityService.Application.UseCases
{
    public class LogoutHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<LogoutHandler> _logger;

        public LogoutHandler(
            ITokenService tokenService,
            IUserRepository userRepository,
            IJwtService jwtService,
            ILogger<LogoutHandler> logger)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _jwtService = jwtService;
            _logger = logger;
        }

        public async Task Handle(string accessToken)
        {
            _logger.LogInformation("Processing logout request with access token: {AccessToken}", accessToken);
            
            var result = await _tokenService.RevokeTokenAsync(accessToken);
            
            if (!result) throw new BadHttpRequestException("Logout failed");
        }
    }
}
