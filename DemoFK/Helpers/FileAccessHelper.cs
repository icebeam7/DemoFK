namespace DemoFK.Helpers
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename) =>
            Path.Combine(FileSystem.AppDataDirectory, filename);
    }
}
