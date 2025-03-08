using System.Security.Claims;
using FluentValidation;
using IdentityService.Application.DTOs.Requests;
using IdentityService.Application.UseCase.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace IdentityService.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : ControllerBase
    {
        private readonly IValidator<ChangePasswordRequest> _changePasswordRequestValidator;
        private readonly IValidator<UpdateUserRequest> _updateUserValidator;
        private readonly GetUserHandler _getUserHandler;
        private readonly UpdateUserHandler _updateUserHandler;
        private readonly ChangePasswordHandler _changePasswordHandler;

        public UserController(
            IValidator<ChangePasswordRequest> changePasswordRequestValidator,
            IValidator<UpdateUserRequest> updateUserValidator,
            GetUserHandler getUserHandler,
            UpdateUserHandler updateUserHandler,
            ChangePasswordHandler changePasswordHandler)
        {
            _changePasswordRequestValidator = changePasswordRequestValidator;
            _updateUserValidator = updateUserValidator;
            _getUserHandler = getUserHandler;
            _updateUserHandler = updateUserHandler;
            _changePasswordHandler = changePasswordHandler;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userProfile = await _getUserHandler.Handle(int.Parse(userId));

            if (userProfile == null) return NotFound();

            return Ok(userProfile);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserRequest request)
        {
            await _updateUserValidator.ValidateAsync(request);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 
            
            var result = await _updateUserHandler.Handle(int.Parse(userId), request);

            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _changePasswordRequestValidator.Validate(request);
            var result = await _changePasswordHandler.Handle(int.Parse(userId!), request);

            if (result == null) return BadRequest("Password change failed");

            return Ok(result);
        }
        
    }
}
