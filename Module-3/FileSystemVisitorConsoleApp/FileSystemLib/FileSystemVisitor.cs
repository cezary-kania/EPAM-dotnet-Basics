using System.Collections;

namespace FileSystemLib;


public sealed class FileSystemVisitor : IEnumerable<string>
{
    private readonly string _rootFolderPath;
    private readonly Func<string, bool>? _filter;
    private readonly IDirectory _directory;

    public event EventHandler<EventArgs>? Start;
    public event EventHandler<EventArgs>? Finish;
    public event EventHandler<FileSystemVisitorEventArgs>? FileFound;
    public event EventHandler<FileSystemVisitorEventArgs>? DirectoryFound;
    public event EventHandler<FileSystemVisitorEventArgs>? FilteredFileFound;
    public event EventHandler<FileSystemVisitorEventArgs>? FilteredDirectoryFound;

    public FileSystemVisitor(IDirectory directory, string? rootFolderPath)
    {
        _directory = directory ?? throw new ArgumentNullException(nameof(directory));
        if (string.IsNullOrEmpty(rootFolderPath) || !_directory.Exists(rootFolderPath))
        {
            throw new ArgumentException("Invalid root folder path.");
        }
        _rootFolderPath = rootFolderPath;
    }

    public FileSystemVisitor(string? rootFolderPath) : this(new FileSystemDirectory(), rootFolderPath)
    {
    }

    public FileSystemVisitor(IDirectory directory, string? rootFolderPath, Func<string, Boolean> filter)
    : this(directory, rootFolderPath)
    {
        _filter = filter;
    }

    public FileSystemVisitor(string? rootFolderPath, Func<string,Boolean> filter) : 
        this(new FileSystemDirectory(), rootFolderPath, filter)
    {
    }

    public IEnumerator<string> GetEnumerator()
    {
        OnStart();
        foreach (var entry in Traverse(_rootFolderPath))
        {
            yield return entry;
        }
        OnFinish();
    }

    private void OnStart() => Start?.Invoke(this, EventArgs.Empty);
    private void OnFinish() => Finish?.Invoke(this, EventArgs.Empty);
    private void OnFileFound(FileSystemVisitorEventArgs e) => FileFound?.Invoke(this, e);
    private void OnDirectoryFound(FileSystemVisitorEventArgs e) => DirectoryFound?.Invoke(this, e);
    private void OnFilteredFileFound(FileSystemVisitorEventArgs e) => FilteredFileFound?.Invoke(this, e);
    private void OnFilteredDirectoryFound(FileSystemVisitorEventArgs e) => FilteredDirectoryFound?.Invoke(this, e);
    public class FileSystemVisitorEventArgs : EventArgs
    {
        public string Entry { get; }
        public bool Exclude { get; set; }
        public bool Abort { get; set; }

        public FileSystemVisitorEventArgs(string entry)
        {
            Entry = entry;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
    
    private IEnumerable<string> Traverse(string path)
    {
        foreach (var entry in _directory.EnumerateFileSystemEntries(path))
        {
            var isDirectory = _directory.Exists(entry);
            var entryFoundArgs = new FileSystemVisitorEventArgs(entry);
            if (isDirectory)
            {
                OnDirectoryFound(entryFoundArgs);
            } else
            {
                OnFileFound(entryFoundArgs);
            }
            if (entryFoundArgs.Exclude)
            {
                continue;
            }
            if (entryFoundArgs.Abort)
            {
                yield break;
            }
            if (_filter is null)
            {
                yield return entry;
            }
            else if (_filter(entry))
            {
                var filteredEntryFoundArgs = new FileSystemVisitorEventArgs(entry);
                if (isDirectory)
                {
                    OnFilteredDirectoryFound(filteredEntryFoundArgs);
                } 
                else
                {
                    OnFilteredFileFound(filteredEntryFoundArgs);
                }
                if (filteredEntryFoundArgs.Exclude)
                {
                    continue;
                }
                if (filteredEntryFoundArgs.Abort)
                {
                    yield break;
                }
                yield return entry;
            }
            if (isDirectory)
            {
                foreach (var child in Traverse(entry))
                {
                    yield return child;
                }
            }
        }
    }
}
