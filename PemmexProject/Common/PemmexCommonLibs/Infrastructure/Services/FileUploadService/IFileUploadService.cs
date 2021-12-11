using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Infrastructure.Services.FileUploadService
{
    public interface IFileUploadService
    {
        public Task FileUploadToAzureAsync(IFormFile file,string fileName);
        public Task<Stream> FileDownloadFromAzureAsync(string fileName);
    }
}
