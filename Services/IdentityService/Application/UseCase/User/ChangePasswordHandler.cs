using IdentityService.Application.DTOs.Requests;
using IdentityService.Application.DTOs.Responses;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Security;
using IdentityService.Application.Interfaces.Services;

namespace IdentityService.Application.UseCases;

public class ChangePasswordHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ICryptographyService _cryptoService;
    private readonly ISerilog<ChangePasswordHandler> _logger;

    public ChangePasswordHandler(IUserRepository userRepository, ICryptographyService cryptoService, ISerilog<ChangePasswordHandler> logger)
    {
        _userRepository = userRepository;
        _cryptoService = cryptoService;
        _logger = logger;
    }

    public async Task<Response<string>> Handle(int userId, ChangePasswordRequest request)
    {
        _logger.LogInformation("Changing password for user with ID: {UserId}", userId);

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("User with ID {UserId} not found", userId);
            throw new KeyNotFoundException("User not found");
        }

        if (!_cryptoService.Verify(user.Password, request.CurrentPassword))
        {
            _logger.LogWarning("Password change failed: Invalid current password for user ID {UserId}", userId);
            throw new BadHttpRequestException("Invalid current password");
        }

        // Hash the new password and update it
        var hashedPassword = _cryptoService.Hash(request.NewPassword);
        user.Password = hashedPassword;

        await _userRepository.UpdateAsync(user);
        _logger.LogInformation("Password for user with ID {UserId} changed successfully.", userId);

        return new Response<string>().Ok(null, "Password changed successfully.");
    }
}