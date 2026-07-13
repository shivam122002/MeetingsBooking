using MeetingsBooking.Application.Interfaces.Repositories;
using MeetingsBooking.Application.Interfaces.Services;
using MeetingsBooking.Domain.Entities;
using MeetingsBooking.Domain.Enums;
using MeetingsBooking.Shared.Dtos;
using System.Text.Json;

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

            Status = MeetingStatus.Scheduled,

            CreatedAt = DateTime.UtcNow
        };

        await _meetingRepository.AddAsync(
            meeting,
            cancellationToken);

        return meeting.Id;
    }

    public async Task<IEnumerable<MeetingResponseDto>> GetAllAsync(
    CancellationToken cancellationToken)
    {
        var meetings = await _meetingRepository.GetAllAsync(
            cancellationToken);

        return meetings.Select(x => new MeetingResponseDto
        {
            Id = x.Id,

            MeetingDate = x.MeetingDate,

            StartTime = x.StartTime,

            DurationInMinutes = x.DurationInMinutes,

            TimeZone = x.TimeZone,

            CreatedAt = x.CreatedAt,
            Status = x.Status,
            MeetingDetails = JsonSerializer.Deserialize<MeetingDetailsDto>(
                            x.MeetingDetailsJson,
                            JsonOptions)!
        });
    }

    public async Task<MeetingResponseDto?> GetByIdAsync(
     Guid id,
     CancellationToken cancellationToken)
    {
        var meeting =
            await _meetingRepository.GetByIdAsync(
                id,
                cancellationToken);

        if (meeting == null)
            return null;

        return new MeetingResponseDto
        {
            Id = meeting.Id,

            MeetingDate = meeting.MeetingDate,

            StartTime = meeting.StartTime,

            DurationInMinutes = meeting.DurationInMinutes,

            TimeZone = meeting.TimeZone,

            CreatedAt = meeting.CreatedAt,
            Status = meeting.Status,
            MeetingDetails =
                JsonSerializer.Deserialize<MeetingDetailsDto>(
                    meeting.MeetingDetailsJson)!
        };
    }
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
}