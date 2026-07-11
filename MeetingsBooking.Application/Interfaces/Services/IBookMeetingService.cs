using MeetingsBooking.Domain.Entities;
using MeetingsBooking.Shared.Dtos;

namespace MeetingsBooking.Application.Interfaces.Services;

public interface IBookMeetingService
{
    Task<Guid> BookMeetingAsync(BookMeetingsRequestDto request,CancellationToken cancellationToken = default);
    Task<IEnumerable<MeetingResponseDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<MeetingResponseDto?> GetByIdAsync(Guid id,CancellationToken cancellationToken);
}