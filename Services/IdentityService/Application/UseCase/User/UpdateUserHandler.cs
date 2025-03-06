using AutoMapper;
using IdentityService.Appication.Events.Movie;
using IdentityService.Application.DTOs.Requests;
using IdentityService.Application.DTOs.Responses;
using IdentityService.Application.Interfaces.Messaging;
using IdentityService.Application.Interfaces.Repositories;
using IdentityService.Application.Interfaces.Services;

namespace IdentityService.Application.UseCases;

public class UpdateUserHandler
{
    private readonly IUserRepository _userRepository;
    private readonly ISerilog<UpdateUserHandler> _logger;
    private readonly IMapper _mapper;
    private readonly INatsPublisher _natsPublisher;
 
    public UpdateUserHandler(IUserRepository userRepository, ISerilog<UpdateUserHandler> logger, IMapper mapper, INatsPublisher natsPublisher)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
        _natsPublisher = natsPublisher;
    }

    public async Task<Response<UserResponse>> Handle(int userId, UpdateUserRequest request)
    {
        _logger.LogInformation("Updating user with ID: {UserId}", userId);

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            _logger.LogWarning("User with ID {UserId} not found", userId);
            throw new KeyNotFoundException("User not found");
        }

        user.Name = request.Name ?? user.Name;
        user.PhoneNumber = request.PhoneNumber ?? user.PhoneNumber;
        user.Address = request.Address ?? user.Address;

        await _userRepository.UpdateAsync(user);
        await _natsPublisher.PublishAsync("user.update", new UserUpdatedEvent
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Address = user.Address,
            PhoneNumber = user.PhoneNumber,
        });
        _logger.LogInformation("User with ID {UserId} updated successfully.", userId);


        return new Response<UserResponse>().Ok(_mapper.Map<UserResponse>(user), "User updated");
    }
}