using MeetingsBooking.Application.Interfaces.Repositories;
using MeetingsBooking.Domain.Entities;
using MeetingsBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MeetingsBooking.Infrastructure;

public class MeetingRepository : IMeetingRepository
{
    private readonly MeetingsBookingDbContext _context;

    public MeetingRepository(
        MeetingsBookingDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Meetings meeting,
        CancellationToken cancellationToken = default)
    {
        await _context.Meetings.AddAsync(
            meeting,
            cancellationToken);

        await _context.SaveChangesAsync(
            cancellationToken);
    }
    public async Task<Meetings?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Meetings
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<Meetings>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Meetings
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}