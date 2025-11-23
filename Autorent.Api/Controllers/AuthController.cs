using Autorent.Infrastructure.Services;
using Autorent.Application.DTO.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Autorent.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _auth.Register(request.Name, request.Email, request.Password);
            return Ok(new { message = result });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var token = await _auth.Login(request.Email, request.Password);
            return Ok(new { token });
        }
    }
}
