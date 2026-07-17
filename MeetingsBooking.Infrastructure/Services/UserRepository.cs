using MeetingsBooking.Application.Interfaces.Repositories;
using MeetingsBooking.Domain.Entities;
using MeetingsBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Infrastructure.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly MeetingsBookingDbContext _context;

        public UserRepository(MeetingsBookingDbContext context)
        {
            _context = context;
        }

        public async Task<bool> EmailExistsAsync(string email,CancellationToken cancellationToken)
        {
            return await _context.Users
                .AnyAsync(x => x.Email == email, cancellationToken);
        }

        public async Task AddAsync( User user,CancellationToken cancellationToken)
        {
            await _context.Users.AddAsync(user, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<User?> GetByEmailAsync(string email,CancellationToken cancellationToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(
                    x => x.Email == email,
                    cancellationToken);
        }
        public async Task<User?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == userId,cancellationToken);
        }   
    }
}
