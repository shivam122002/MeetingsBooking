using MeetingsBooking.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Application.Interfaces.Services
{
    public interface IPasswordHasher
    {
        string HashPassword(User user, string password);
        bool VerifyPassword(User user,string hashedPassword,string providedPassword);
    }
}
