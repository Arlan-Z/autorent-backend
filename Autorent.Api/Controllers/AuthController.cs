using Autorent.Application.DTO.Auth;
using Autorent.Application.Interfaces;
using Autorent.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Autorent.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.Register(request.Name, request.Email, request.Password);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _authService.Login(request.Email, request.Password);
            return Ok(new { token });
        }
    }
}
