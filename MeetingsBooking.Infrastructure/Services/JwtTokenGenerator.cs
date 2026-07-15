using MeetingsBooking.Application.Interfaces.Services;
using MeetingsBooking.Domain.Entities;
using MeetingsBooking.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MeetingsBooking.Domain;

namespace MeetingsBooking.Infrastructure.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly JwtSettings _jwtSettings;

        public JwtTokenGenerator(
            IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }
        public AccessTokenResult GenerateAccessToken(User user)
        {
            //throw new NotImplementedException();
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            
                new Claim(JwtRegisteredClaimNames.Name,
                    $"{user.FirstName} {user.LastName}"),
            
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var credentials = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                          issuer: _jwtSettings.Issuer,
                          audience: _jwtSettings.Audience,
                          claims: claims,
                          expires: DateTime.UtcNow.AddMinutes(
                              _jwtSettings.AccessTokenExpiryMinutes),
                          signingCredentials: credentials);

            var tokenHandler =new JwtSecurityTokenHandler();
            return new AccessTokenResult
            {
                Token = tokenHandler.WriteToken(token),
                ExpiresAt = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpiryMinutes)
            };
        }
        public RefreshToken GenerateRefreshToken()
        {
            var randomBytes = new byte[64];

            RandomNumberGenerator.Fill(randomBytes);

            var token = Convert.ToBase64String(randomBytes);

            return new RefreshToken
            {
                Id = Guid.NewGuid(),

                Token = token,

                CreatedAt = DateTime.UtcNow,

                ExpiresAt = DateTime.UtcNow.AddDays(
                    _jwtSettings.RefreshTokenExpiryDays),

                IsRevoked = false
            };
        }
    }
}
