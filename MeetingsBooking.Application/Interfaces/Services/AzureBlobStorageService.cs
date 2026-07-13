using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MeetingsBooking.Application.Interfaces.Repositories;
using MeetingsBooking.Application.Interfaces.Services;

namespace MeetingsBooking.Application.Services;

public class AzureBlobStorageService : IAzureBlobStorageService
{
	private readonly IAzureBlobStorageRepository _azureBlobStorageRepository;

	public AzureBlobStorageService(IAzureBlobStorageRepository azureBlobStorageRepository)
	{
		_azureBlobStorageRepository = azureBlobStorageRepository ?? throw new ArgumentNullException(nameof(azureBlobStorageRepository));
	}

	public async Task UploadFileAsync(string fileName, Stream fileStream)
	{
		await _azureBlobStorageRepository.UploadFileAsync(fileName, fileStream).ConfigureAwait(false);
	}

	public async Task<Stream> DownloadFileAsync(string fileName)
	{
		return await _azureBlobStorageRepository.DownloadFileAsync(fileName).ConfigureAwait(false);
	}

	public async Task DeleteFileAsync(string fileName)
	{
		await _azureBlobStorageRepository.DeleteFileAsync(fileName).ConfigureAwait(false);
	}

	public async Task<List<string>> ListFilesAsync()
	{
		return await _azureBlobStorageRepository.ListFilesAsync().ConfigureAwait(false);
	}
}
