using MeetingsBooking.Domain.Entities;

namespace MeetingsBooking.Application.Interfaces.Repositories;

public interface IMeetingRepository
{
    Task AddAsync(
        Meetings meeting,
        CancellationToken cancellationToken = default);
    Task<Meetings?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default); 
    Task<IEnumerable<Meetings>> GetAllAsync(
        CancellationToken cancellationToken = default); 
}