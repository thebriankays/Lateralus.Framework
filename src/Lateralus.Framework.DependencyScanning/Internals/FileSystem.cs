namespace Lateralus.Framework.DependencyScanning.Internals;

internal sealed class FileSystem : IFileSystem
{
    private FileSystem()
    {
    }

    public static IFileSystem Instance { get; } = new FileSystem();

    public IEnumerable<string> GetFiles(string path, string pattern, SearchOption searchOptions)
    {
        return Directory.EnumerateFiles(path, pattern, searchOptions);
    }

    public Stream OpenRead(string path)
    {
        return File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
    }

    public Stream OpenReadWrite(string path)
    {
        return File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
    }
}
