using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingsBooking.Domain.Enums
{
    public enum MeetingStatus
    {
        Scheduled = 1,      // Booking created

        Pending = 2,        // Waiting for host approval

        Accepted = 3,       // Host accepted

        InProgress = 4,     // Meeting is live

        Completed = 5,      // Meeting finished

        Cancelled = 6,      // Cancelled

        Expired = 7         // Missed / never joined
    }
}
