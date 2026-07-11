using MeetingsBooking.Shared.Dtos;
using System.Net.Http.Json;

public interface IBookMeetings
{
    Task<BookMeetingsResponse> BookMeetingAsync(BookMeetingsRequestDto request);
    Task<List<MeetingResponseDto>> GetAllMeetingsAsync();
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
    public async Task<List<MeetingResponseDto>> GetAllMeetingsAsync()
    {
        try
        {
            var meetings = await _httpClient.GetFromJsonAsync<List<MeetingResponseDto>>(
                "api/meetings/getAllmeetings");

            return meetings ?? new List<MeetingResponseDto>();
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);

            return new List<MeetingResponseDto>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);

            return new List<MeetingResponseDto>();
        }
    }
}