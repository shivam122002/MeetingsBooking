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

        public AuthenticationService(IUserRepository userRepository,IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
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
    }
}
