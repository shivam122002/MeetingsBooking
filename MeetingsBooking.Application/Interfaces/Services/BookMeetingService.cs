using MeetingsBooking.Application.Interfaces.Repositories;
using MeetingsBooking.Application.Interfaces.Services;
using MeetingsBooking.Domain.Entities;
using MeetingsBooking.Shared.Dtos;

namespace MeetingsBooking.Application.Services;

public class BookMeetingService : IBookMeetingService
{
    private readonly IMeetingRepository _meetingRepository;

    public BookMeetingService(
        IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    public async Task<Guid> BookMeetingAsync(
        BookMeetingsRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var meeting = new Meetings
        {
            Id = Guid.NewGuid(),

            MeetingDate = request.MeetingDate,

            StartTime = request.StartTime,

            DurationInMinutes = request.DurationInMinutes,

            TimeZone = request.TimeZone,

            MeetingDetailsJson = request.MeetingDetailsJson,

            CreatedAt = DateTime.UtcNow
        };

        await _meetingRepository.AddAsync(
            meeting,
            cancellationToken);

        return meeting.Id;
    }
}