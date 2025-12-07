using Autorent.Domain.DTOs.Auth;
using Autorent.Domain.Exceptions;
using Autorent.Domain.Interfaces;
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
            try
            {
                var result = await _authService.Register(request.Name, request.Email, request.Password);
                return Ok(new { message = result });
            }
            catch (UserAlreadyExistsException userEx)
            {
                return Conflict(new { message = userEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var token = await _authService.Login(request.Email, request.Password);
                return Ok(new { token });
            }
            catch (InvalidCredentialsException credEx)
            {
                return Unauthorized(new { message = "Invalid credentials", error = credEx.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}
