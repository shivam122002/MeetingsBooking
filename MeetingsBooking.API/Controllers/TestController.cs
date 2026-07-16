using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeetingsBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet("public")]
        public IActionResult Public()
        {
            return Ok("Public API");
        }
        [Authorize]
        [HttpGet("private")]
        public IActionResult Private()
        {
            return Ok(new {
                Name=User.Identity?.Name,
                Role=User.FindFirst(ClaimTypes.Role)?.Value
            });
        }
    }
}
