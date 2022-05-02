using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using WeGrow.Extensions;
using WeGrow.Interfaces;

namespace WeGrow.Services
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient blobServiceClient;
        public BlobService(BlobServiceClient blobServiceClient)
        {
            this.blobServiceClient = blobServiceClient;
        }

        public async Task<BlobDownloadResult> GetBlobAsync(string containerName, string blobName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            var blobDownload = await blobClient.DownloadContentAsync();
            return blobDownload;
        }

        public Uri GetBlobLinkAsync(string containerName, string blobName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            return blobClient.Uri;
        }

        public async Task<IEnumerable<BlobItem>> ListBlobsAsync(string containerName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var items = new List<BlobItem>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                items.Add(blobItem);
            }
            return items;
        }

        public async Task<BlobContentInfo> UploadFileBlobAsync(string containerName, string blobName, byte[] bytes, string contentType = null)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await using var memoryStream = new MemoryStream(bytes);
            return await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders { ContentType = contentType ?? blobName.GetContentType()});
        }

        public async Task DeleteBlobAsync(string containerName, string blobName)
        {
            var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}
