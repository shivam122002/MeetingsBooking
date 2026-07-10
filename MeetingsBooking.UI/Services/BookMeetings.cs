using MeetingsBooking.Shared.Dtos;

public interface IBookMeetings
{
    Task<BookMeetingsResponse> BookMeetingAsync(BookMeetingsRequestDto request);
} 
public class BookMeetings : IBookMeetings
{
    public async Task<BookMeetingsResponse> BookMeetingAsync(BookMeetingsRequestDto request)
    {
        // Implement the logic to book a meeting here
        // For example, you can call an API or save the meeting details to a database
        // Simulating a successful booking operation
        await Task.Delay(1000); // Simulate some asynchronous operation
        return new BookMeetingsResponse
        {
            IsSuccess = true,
            Message = "Meeting booked successfully."
        };
    }
}   