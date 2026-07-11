using MeetingsBooking.Application.Interfaces.Repositories;
using MeetingsBooking.Domain.Entities;
using MeetingsBooking.Infrastructure.Persistence;

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
}