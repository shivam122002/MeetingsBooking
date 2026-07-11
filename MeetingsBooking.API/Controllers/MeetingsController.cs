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
        [HttpGet("getAllmeetings")]
        public async Task<IActionResult> GetAllMeetings(CancellationToken cancellationToken)
        {
            var meetings = await _bookMeetingService.GetAllAsync(cancellationToken);
            return Ok(meetings);
        }
        [HttpGet("get-meeting/{id}")]
        public async Task<IActionResult> GetMeeting([FromRoute] Guid id,CancellationToken cancellationToken)
        {
            var meeting = await _bookMeetingService.GetByIdAsync(id, cancellationToken);
            if (meeting == null)
            {
                return NotFound();
            }
            return Ok(meeting);
        }   
    }
}

