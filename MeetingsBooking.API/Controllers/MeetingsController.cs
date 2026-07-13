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
        private readonly IAzureBlobStorageService _blobStorageService;
        public MeetingsController(
            IBookMeetingService bookMeetingService, IAzureBlobStorageService blobStorageService  )
        {
            _bookMeetingService = bookMeetingService;
            _blobStorageService = blobStorageService;
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
        [HttpPost("upload-file")]
        public async Task<IActionResult> UploadFile(IFormFile file,CancellationToken cancellationToken)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not found.");

            using var stream = file.OpenReadStream();

            await _blobStorageService.UploadFileAsync(file.FileName, stream);

            return Ok("Done");
        }
        [HttpGet("getAllFiles")]
        public async Task<IActionResult> GetAllFiles(CancellationToken cancellationToken)
        {
            var files = await _blobStorageService.ListFilesAsync();

            return Ok(files);
        }
    }
}

