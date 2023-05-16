using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemLib;

public class FileSystemDirectory : IDirectory
{
    public IEnumerable<string> EnumerateFileSystemEntries(string path)
    {
        return Directory.EnumerateFileSystemEntries(path);
    }

    public bool Exists(string path)
    {
        return Directory.Exists(path);
    }
}
