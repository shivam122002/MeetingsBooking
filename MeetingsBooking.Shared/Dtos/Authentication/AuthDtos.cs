using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Shared.Dtos.Authentication
{
    public class RegisterRequestDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
    public class RegisterResponseDto
    {
        public Guid UserId { get; set; }

        public string Message { get; set; } = string.Empty;
    }
    public class LoginRequestDto
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
    public class LoginResponseDto
    {
        public string?AccessToken { get; set; }

        public string?RefreshToken { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
    public class RefreshTokenRequestDto
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
