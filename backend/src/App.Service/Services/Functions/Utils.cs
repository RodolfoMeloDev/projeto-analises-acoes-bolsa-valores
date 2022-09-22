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
    }
}
