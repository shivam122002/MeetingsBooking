using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Shared.Dtos
{
    public class MeetingResponseDto
    {
        public Guid Id { get; set; }

        public DateTime MeetingDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public int DurationInMinutes { get; set; }

        public string TimeZone { get; set; } = string.Empty;

        public MeetingDetailsDto MeetingDetails { get; set; } = new();

        public DateTime CreatedAt { get; set; }
        public bool IsExpanded { get; set; }
    }
    public class BookMeetingsRequestDto
    {
        public DateTime MeetingDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public int DurationInMinutes { get; set; }

        public string TimeZone { get; set; } = string.Empty;

        public string MeetingDetailsJson { get; set; } = string.Empty;
    }
    public class MeetingDetailsDto
    {
        public string MeetingTitle { get; set; } = string.Empty;

        public string MeetingReason { get; set; } = string.Empty;

        public List<string> DiscussionTopics { get; set; } = [];

        public string AdditionalNotes { get; set; } = string.Empty;
    }
    public class BookMeetingsResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }   
}
