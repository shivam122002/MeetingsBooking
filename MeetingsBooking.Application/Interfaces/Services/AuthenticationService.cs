using MeetingsBooking.Application.Interfaces.Repositories;
using MeetingsBooking.Domain.Entities;
using MeetingsBooking.Domain.Enums;
using MeetingsBooking.Shared.Dtos.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Application.Interfaces.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;

        private readonly IPasswordHasher _passwordHasher;

        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IRefreshTokenRepository _refreshTokenRepository;        
        public AuthenticationService(IUserRepository userRepository,IPasswordHasher passwordHasher,IJwtTokenGenerator jwtTokenGenerator,IRefreshTokenRepository refreshTokenRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
            _refreshTokenRepository = refreshTokenRepository;
        }
        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken)
        {
            request.Email =
                request.Email.Trim().ToLowerInvariant();

            if (await _userRepository.EmailExistsAsync(request.Email, cancellationToken))
            {
                throw new Exception("Email already exists.");
            }
            var user = new User
            {
                FirstName = request.FirstName.Trim(),
                LastName = request.LastName.Trim(),
                Email = request.Email,
                Role = Roles.Employee,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            user.PasswordHash =
                _passwordHasher.HashPassword(
                    user,
                    request.Password);
            await _userRepository.AddAsync(
                user,
                cancellationToken);

            return new RegisterResponseDto
            {
                UserId = user.Id,
                Message = "User registered successfully."
            };
        }
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request,CancellationToken cancellationToken)
        {
            request.Email = request.Email.Trim().ToLowerInvariant();
            var user = await _userRepository.GetByEmailAsync(request.Email,cancellationToken);
            if (user is null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            var isPasswordValid =_passwordHasher.VerifyPassword(user,user.PasswordHash,request.Password);
            if (!isPasswordValid)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            if (!user.IsActive)
            {
                throw new UnauthorizedAccessException("Your account is inactive.");
            }
            var accessTokenResult = _jwtTokenGenerator.GenerateAccessToken(user);   
            var accessToken = accessTokenResult.Token;//_jwtTokenGenerator.GenerateAccessToken(user);
            var refreshToken =_jwtTokenGenerator.GenerateRefreshToken();
            refreshToken.UserId = user.Id;
            await _refreshTokenRepository.AddAsync(refreshToken,cancellationToken);
            return new LoginResponseDto
            {
                AccessToken = accessToken,

                RefreshToken = refreshToken.Token,
                ExpiresAt = accessTokenResult.ExpiresAt
            };
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellationToken)
        {
           // throw new NotImplementedException();
            var existingRefreshToken = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
            if (existingRefreshToken is null || existingRefreshToken.IsRevoked || existingRefreshToken.ExpiresAt <= DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }
            var user = await _userRepository.GetUserByIdAsync(existingRefreshToken.UserId, cancellationToken);
            if (user is null || !user.IsActive)
            {
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");
            }
            var accessTokenResult = _jwtTokenGenerator.GenerateAccessToken(user);
            var accessToken = accessTokenResult.Token;
            var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(); 
            refreshToken.UserId = user.Id;    
            await _refreshTokenRepository.RevokeRefreshTokenAsync(existingRefreshToken, cancellationToken);
            await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);    
            return new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = accessTokenResult.ExpiresAt
            };
        }
    }
}
