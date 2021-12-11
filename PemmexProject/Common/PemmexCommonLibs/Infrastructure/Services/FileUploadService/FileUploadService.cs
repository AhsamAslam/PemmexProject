using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using PemmexCommonLibs.Application.Helpers;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using Azure.Storage.Blobs;

namespace PemmexCommonLibs.Infrastructure.Services.FileUploadService
{
    public class FileUploadService: IFileUploadService
    {
        private CloudStorageAccount _storageAccount;
        private string _containerName = "";
        private string _connectionString = "";

        public FileUploadService(AzureContainerSettings azureContainerSettings)
        {
            _storageAccount = CloudStorageAccount.Parse(azureContainerSettings.connectionString);
            _containerName = azureContainerSettings.containerName;
            _connectionString = azureContainerSettings.connectionString;
        }
        public async Task FileUploadToAzureAsync(IFormFile file,string fileName)
        {
            var cloudBlobClient = _storageAccount.CreateCloudBlobClient();
            var cloudBlobContainer = cloudBlobClient.GetContainerReference(_containerName);
            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                await cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Container
                });
            }
            var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(fileName);
            cloudBlockBlob.Properties.ContentType = file.ContentType;
            await cloudBlockBlob.UploadFromStreamAsync(file.OpenReadStream());
        }
        public async Task<Stream> FileDownloadFromAzureAsync(string fileName)
        {
            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            var blobClient = containerClient.GetBlobClient(fileName);
            if (await blobClient.ExistsAsync())
            {
                var response = await blobClient.DownloadAsync();
                return response.Value.Content;
            }
            else
            {
                return null;
            }
        }
    }
}
