using MeetingsBooking.Shared.Dtos;

namespace MeetingsBooking.Application.Interfaces.Services;

public interface IBookMeetingService
{
    Task<Guid> BookMeetingAsync(
        BookMeetingsRequestDto request,
        CancellationToken cancellationToken = default);
}