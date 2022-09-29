using Microsoft.AspNetCore.Http;

namespace App.Service.Services.Functions
{
    public static class Utils
    {
        public static string CreateDirectory(string directory)
        {
            string? strWorkPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            if (!Directory.Exists(strWorkPath + '\\' + directory))
                Directory.CreateDirectory(strWorkPath + '\\' + directory);

            return strWorkPath + '\\' + directory;
        }

        public static string CopyFileToServer(IFormFile file, string directoryFile)
        {
            string pathFile;
            Utils.CreateDirectory("Users");
            pathFile = Utils.CreateDirectory("Users\\" + directoryFile) + "\\" + file.FileName;

            using (FileStream fileStream = System.IO.File.Create(pathFile))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }

            return pathFile;
        }
    }
}
