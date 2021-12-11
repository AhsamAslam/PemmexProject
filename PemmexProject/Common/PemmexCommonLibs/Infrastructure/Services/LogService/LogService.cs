using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Infrastructure.Services.LogService
{
    public class LogService : ILogService
    {
        private CloudStorageAccount _storageAccount;
        private string _containerName = "";
        private string _connectionString = "";

        public LogService(AzureContainerSettings azureContainerSettings)
        {
            _storageAccount = CloudStorageAccount.Parse(azureContainerSettings.connectionString);
            _containerName = azureContainerSettings.containerName;
            _connectionString = azureContainerSettings.connectionString;
        }
        public async Task WriteLogAsync(Exception ex, string fileName)
        {
            var cloudBlobClient = _storageAccount.CreateCloudBlobClient();
            // take Container's reference
            var container = cloudBlobClient.GetContainerReference(_containerName);

            fileName += ".txt";
            // take Blob's reference to modify
            var blob = container.GetAppendBlobReference(fileName);

            // verify the existance of blob
            bool isPresent = await blob.ExistsAsync();

            // if blob doesn't exist, the system will create it
            if (!isPresent)
            {
                await blob.CreateOrReplaceAsync();
            }

            // append the new value and new line
            await blob.AppendTextAsync($"{DateTime.Now}\n{ex.ToString()} \n\n");

        }
    }
}
