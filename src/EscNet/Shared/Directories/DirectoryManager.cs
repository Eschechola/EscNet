using System.IO;

namespace EscNet.Shared.Directories;

public static class DirectoryManager
{
    public static void CreateDirectoryIfNotExists(string path)
    {
        if (path != null && !Directory.Exists(path))
            Directory.CreateDirectory(path);
    }
}