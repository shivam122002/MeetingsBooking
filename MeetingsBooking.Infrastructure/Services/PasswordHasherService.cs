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

        public string HashPassword(string password)
        {
            var user = new User();
            return _passwordHasher.HashPassword(
                user,
                password);
        }
        public bool VerifyPassword(string hashedPassword,string providedPassword)
        {
            var user = new User();

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                hashedPassword,
                providedPassword);

            return result != PasswordVerificationResult.Failed;
        }
    }
}
