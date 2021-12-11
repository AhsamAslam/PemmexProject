using System.IO;

namespace PemmexCommonLibs.Application.Helpers
{
    public static class FileHelper
    {
        public static void UploadFile(string fileName)
        {
            if (!Directory.Exists(fileName))
            {
                Directory.CreateDirectory(fileName);
            }
        }
    }

}
