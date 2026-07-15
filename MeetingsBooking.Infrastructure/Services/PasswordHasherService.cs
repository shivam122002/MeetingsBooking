using MeetingsBooking.Application.Interfaces.Services;
using MeetingsBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Infrastructure.Services
{
    public class PasswordHasherService : IPasswordHasher
    {
        private readonly PasswordHasher<User> _passwordHasher = new();

        public string HashPassword(User user,string password)
        {
            return _passwordHasher.HashPassword(
                user,
                password);
        }
        public bool VerifyPassword(User user,string hashedPassword,string providedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                hashedPassword,
                providedPassword);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
