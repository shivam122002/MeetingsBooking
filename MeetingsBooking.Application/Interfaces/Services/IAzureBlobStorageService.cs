namespace MeetingsBooking.Application.Interfaces.Services;
public interface IAzureBlobStorageService
{
    Task UploadFileAsync(string fileName, Stream fileStream);
    Task<Stream> DownloadFileAsync(string fileName);
    Task DeleteFileAsync(string fileName);
    Task<List<string>> ListFilesAsync();

}
