using MeetingsBooking.Application.Interfaces.Services;
using MeetingsBooking.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MeetingsBooking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IBookMeetingService _bookMeetingService;

        public MeetingsController(
            IBookMeetingService bookMeetingService)
        {
            _bookMeetingService = bookMeetingService;
        }

        [HttpPost("book-meeting")]
        public async Task<IActionResult> BookMeeting(
            [FromBody] BookMeetingsRequestDto request,
            CancellationToken cancellationToken)
        {
            var meetingId =
                await _bookMeetingService.BookMeetingAsync(
                    request,
                    cancellationToken);

            return CreatedAtAction(
                nameof(BookMeeting),
                new
                {
                    id = meetingId
                },
                new
                {
                    meetingId
                });
        }
    }
}
