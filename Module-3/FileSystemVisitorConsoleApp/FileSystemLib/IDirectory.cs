namespace FileSystemLib;

public interface IDirectory
{
    IEnumerable<string> EnumerateFileSystemEntries(string path);
    bool Exists(string path);
}
