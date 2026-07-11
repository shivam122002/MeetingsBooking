using MeetingsBooking.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Domain.Entities
{
    public class Meetings
    {
        public Guid Id { get; set; }

        public DateTime MeetingDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public int DurationInMinutes { get; set; }

        public string TimeZone { get; set; } = string.Empty;

        public MeetingStatus Status { get; set; } = MeetingStatus.Pending;

        public string MeetingDetailsJson { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
