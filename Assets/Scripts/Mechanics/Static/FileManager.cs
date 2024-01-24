using System.IO;

public class FileManager
{
    public static void DeleteDirectory(string path)
    {
        if(Directory.Exists(path) == false)
            return;

        foreach (string file in Directory.GetFiles(path))
            File.Delete(file);

        foreach (string subdirectory in Directory.GetDirectories(path))
            DeleteDirectory(subdirectory);

        Directory.Delete(path);
    }
}