using MeetingsBooking.Domain.Entities;

namespace MeetingsBooking.Application.Interfaces.Repositories;

public interface IMeetingRepository
{
    Task AddAsync(
        Meetings meeting,
        CancellationToken cancellationToken = default);
}