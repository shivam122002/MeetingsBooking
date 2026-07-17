using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MeetingsBooking.Application.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;

namespace MeetingsBooking.Infrastructure
{
    public class AzureBlobStorageRepository : IAzureBlobStorageRepository
    {
        private readonly BlobServiceClient blobServiceClient;
        private readonly string containerName; // Replace with your actual container name
        public AzureBlobStorageRepository(IConfiguration configuration) 
        {
            var connectionString =
     configuration["AzureBlobStorage:AzureBlobStorageString"]
     ?? throw new InvalidOperationException(
         "AzureBlobStorage:AzureBlobStorageString is required.");

            containerName =
                configuration["AzureBlobStorage:ContainerName"]
                ?? throw new InvalidOperationException(
                    "AzureBlobStorage:ContainerName is required.");

            try
            {
                blobServiceClient = new BlobServiceClient(connectionString);
            }
            catch(Exception ex)
            {

            }
        }
        private async Task<BlobContainerClient> GetContainerClientAsync()
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync(PublicAccessType.None);
            return containerClient;
        }
        public async Task DeleteFileAsync(string fileName)
        {
            var containerClient = await GetContainerClientAsync();
            var blobClient = containerClient.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }

        public async Task<Stream> DownloadFileAsync(string fileName)
        {
            var containerClient = await GetContainerClientAsync();
            var blobClient = containerClient.GetBlobClient(fileName);
            var response = await blobClient.DownloadAsync();
            return response.Value.Content;
        }
        public async Task<List<string>> ListFilesAsync()
        {
            var containerClient = await GetContainerClientAsync();
            var blobs = new List<string>();
            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                blobs.Add(blobItem.Name);
            }
            return blobs;
        }
        public async Task UploadFileAsync(string fileName, Stream fileStream)
        {
            if (fileStream.CanSeek)
            {
                fileStream.Position = 0;
            }
            var containerClient = await GetContainerClientAsync();
            var blobClient = containerClient.GetBlobClient(fileName);

            var uploadOptions = new BlobUploadOptions
            {
                TransferOptions = new StorageTransferOptions
                {
                    MaximumConcurrency = 1
                }
            };
            await blobClient.UploadAsync(fileStream, uploadOptions);
        }
    }
}
