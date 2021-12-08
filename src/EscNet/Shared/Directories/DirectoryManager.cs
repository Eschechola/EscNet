using System.IO;

namespace EscNet.Shared.Directories
{
    public static class DirectoryManager
    {
        public static bool CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return true;
        }
    }
}
