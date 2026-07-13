

namespace MeetingsBooking.Application.Interfaces.Repositories
{
    public interface IAzureBlobStorageRepository
    {
        Task UploadFileAsync(string fileName, Stream fileStream);
        Task<Stream> DownloadFileAsync(string fileName);
        Task DeleteFileAsync(string fileName);
        Task<List<string>> ListFilesAsync();    
    }
}
