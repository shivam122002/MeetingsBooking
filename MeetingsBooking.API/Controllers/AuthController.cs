using MeetingsBooking.Application.Interfaces.Services;
using MeetingsBooking.Shared.Dtos.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MeetingsBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        
        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            return Ok(new
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,

                Email = User.FindFirst(ClaimTypes.Email)?.Value,

                Name = User.Identity?.Name,

                Role = User.FindFirst(ClaimTypes.Role)?.Value,

                IsAuthenticated = User.Identity?.IsAuthenticated,

                AuthenticationType = User.Identity?.AuthenticationType
            });
        }
        [HttpPost("register")]   
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.RegisterAsync(request, cancellationToken);
            return Ok(response);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.LoginAsync(request, cancellationToken);
            return Ok(response);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto request, CancellationToken cancellationToken)
        {
            var response = await _authenticationService.RefreshTokenAsync(request, cancellationToken);
            return Ok(response);
        }
    }
}
