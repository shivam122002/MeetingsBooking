using MeetingsBooking.Shared.Dtos;
using System.Net.Http.Json;

public interface IBookMeetings
{
    Task<BookMeetingsResponse> BookMeetingAsync(BookMeetingsRequestDto request);
}
public class BookMeetings : IBookMeetings
{
    private readonly HttpClient _httpClient;

    public BookMeetings(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<BookMeetingsResponse> BookMeetingAsync(
        BookMeetingsRequestDto request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/meetings/book-meeting",
                request);

            if (!response.IsSuccessStatusCode)
            {
                return new BookMeetingsResponse
                {
                    IsSuccess = false,
                    Message = "Unable to book meeting."
                };
            }

            return new BookMeetingsResponse
            {
                IsSuccess = true,
                Message = "Meeting booked successfully."
            };
        }
        catch (HttpRequestException ex)
        {
            return new BookMeetingsResponse
            {
                IsSuccess = false,
                Message = ex.Message
            };
        }
    }
}