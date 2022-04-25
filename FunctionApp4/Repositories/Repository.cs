using System.Threading.Tasks;
using Azure.Storage.Blobs;
using FunctionApp4.Configurations;
using Microsoft.Extensions.Options;

namespace FunctionApp4.Repositories;

public class Repository : IRepository
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly BlobStorageOptions _blobStorageOptions;

    public Repository(BlobServiceClient blobServiceClient, IOptions<BlobStorageOptions> options)
    {
        _blobServiceClient = blobServiceClient;
        _blobStorageOptions = options.Value;
    }

    public async Task<bool> DeletePhotoAsync(string photoName)
    {
        var container = _blobServiceClient.GetBlobContainerClient(_blobStorageOptions.ContainerName);
        var blobClient = container.GetBlobClient(photoName);
        var response = await blobClient.DeleteIfExistsAsync();
        return response.Value;
    }
}