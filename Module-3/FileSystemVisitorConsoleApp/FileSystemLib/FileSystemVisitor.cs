using System.Collections;

namespace FileSystemLib;


public sealed class FileSystemVisitor : IEnumerable<string>
{
    private readonly string _rootFolderPath;
    private readonly Func<string, bool>? _filter;
    private readonly IDirectory _directory;

    public event EventHandler<EventArgs>? OnStart;
    public event EventHandler<EventArgs>? OnFinish;
    public event EventHandler<FileSystemVisitorEventArgs>? OnFileFound;
    public event EventHandler<FileSystemVisitorEventArgs>? OnDirectoryFound;
    public event EventHandler<FileSystemVisitorEventArgs>? OnFilteredFileFound;
    public event EventHandler<FileSystemVisitorEventArgs>? OnFilteredDirectoryFound;

    public FileSystemVisitor(IDirectory directory, string? rootFolderPath)
    {
        _directory = directory ?? throw new ArgumentNullException(nameof(directory));
        if (string.IsNullOrEmpty(rootFolderPath) || !_directory.Exists(rootFolderPath))
        {
            throw new ArgumentException("Invalid root folder path.", nameof(rootFolderPath));
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
        OnStart?.Invoke(this, EventArgs.Empty);
        foreach (var entry in Traverse(_rootFolderPath))
        {
            yield return entry;
        }
        OnFinish?.Invoke(this, EventArgs.Empty);
    }

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
                OnDirectoryFound?.Invoke(this, entryFoundArgs);
            } else
            {
                OnFileFound?.Invoke(this, entryFoundArgs);
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
                    OnFilteredDirectoryFound?.Invoke(this, filteredEntryFoundArgs);
                } 
                else
                {
                    OnFilteredFileFound?.Invoke(this, filteredEntryFoundArgs);
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
