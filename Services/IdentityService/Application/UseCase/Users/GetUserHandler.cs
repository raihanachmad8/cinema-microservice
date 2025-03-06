using AutoMapper;
using IdentityService.Application.DTOs.Responses;
using IdentityService.Application.Interfaces.Repositories;

namespace IdentityService.Application.UseCase.Users
{
    public class GetUserHandler
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        // Constructor to inject the IUserService dependency
        public GetUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Response<UserResponse?>> Handle(int id)
        {
            // Fetch the user profile from the user service
            var userProfile = await _userRepository.GetByIdAsync(id);

            if (userProfile == null)
            {
                throw new KeyNotFoundException("User not found");
            }
            return new Response<UserResponse>().Ok(_mapper.Map<UserResponse>(userProfile), "Get User");
        }
    }
}