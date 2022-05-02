using Azure.Storage.Blobs.Models;

namespace WeGrow.Interfaces
{
    public interface IBlobService
    {
        public Task<BlobDownloadResult> GetBlobAsync(string containerName, string blobName);

        public Uri GetBlobLinkAsync(string containerName, string blobName);

        public Task<IEnumerable<BlobItem>> ListBlobsAsync(string containerName);

        public Task<BlobContentInfo> UploadFileBlobAsync(string containerName, string blobName, byte[] bytes, string contentType = null);

        public Task DeleteBlobAsync(string containerName, string blobName);
    }
}
