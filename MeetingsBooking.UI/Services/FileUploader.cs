using Microsoft.AspNetCore.Components;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MeetingsBooking.UI.Services;

public interface IFileUploader
{
    Task<string?> UploadFileAsync(string fileName,
        Stream fileStream
        );
    Task<List<string>> ListFilesAsync();
}

public class FileUploader : IFileUploader
{
    private readonly HttpClient _httpClient;

    public FileUploader(
        HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string?> UploadFileAsync(string fileName,Stream fileStream)
    {
        using var content = new MultipartFormDataContent();

        using var streamContent =
            new StreamContent(fileStream);

        streamContent.Headers.ContentType =
            new MediaTypeHeaderValue(
                "application/octet-stream");

        content.Add(
            streamContent,
            "file",
            fileName);

        var response =
            await _httpClient.PostAsync(
                "api/meetings/upload-file",
                content);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content
            .ReadFromJsonAsync<string>();
    }
    public async Task<List<string>> ListFilesAsync()
    {
        var response = await _httpClient.GetAsync("api/meetings/getAllFiles");

        if (!response.IsSuccessStatusCode)
            return new List<string>();

        return await response.Content.ReadFromJsonAsync<List<string>>()
               ?? new List<string>();
    }
}