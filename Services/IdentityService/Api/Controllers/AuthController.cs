using IdentityService.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Api.Controllers
{
    [Route("/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoggerService<AuthController> _logger;
        public AuthController(ILoggerService<AuthController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            _logger.LogInformasi("Test");
            return Ok();
        }
    }
}