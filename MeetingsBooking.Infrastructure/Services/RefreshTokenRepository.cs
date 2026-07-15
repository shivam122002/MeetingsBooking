using MeetingsBooking.Application.Interfaces.Repositories;
using MeetingsBooking.Domain.Entities;
using MeetingsBooking.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Infrastructure.Services
{
    public class RefreshTokenRepository
    : IRefreshTokenRepository
    {
        private readonly MeetingsBookingDbContext _context;

        public RefreshTokenRepository(
            MeetingsBookingDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(RefreshToken refreshToken,CancellationToken cancellationToken)
        {
            _context.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync(cancellationToken); 
        }
    }
}
