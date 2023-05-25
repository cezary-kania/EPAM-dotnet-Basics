using FileSystemLib;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests;

public class FileSystemVisitorTests
{
    private readonly Mock<IDirectory> _directoryMock;

    public FileSystemVisitorTests()
    {
        _directoryMock = new Mock<IDirectory>();
    }
    [Fact]
    public void FileSystemVisitor_ThrowsArgumentException_WhenRootDirectoryDoesNotExist()
    {
        // Arrange
        _directoryMock.SetupSequence(x => x.Exists(It.IsAny<string>()))
            .Returns(false);

        // Act
        var constructVisitorAction = () => new FileSystemVisitor(
            _directoryMock.Object, "C:\\TestDir");
        
        // Assert
        constructVisitorAction
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Invalid root folder path. (Parameter 'rootFolderPath')");
    }

    [Fact]
    public void FileSystemVisitor_ThrowsArgumentException_WhenRootDirectoryArgumentIsEmptyString()
    {
        // Act
        var constructVisitorAction = () => new FileSystemVisitor(
            _directoryMock.Object, 
            string.Empty);

        // Assert
        constructVisitorAction
            .Should()
            .Throw<ArgumentException>()
            .WithMessage("Invalid root folder path. (Parameter 'rootFolderPath')");
    }
    
    [Fact]
    public void GetEnumerator_ShouldTraverseAllFilesAndDirectoriesInRootFolder_WhenItIsNotEmpty()
    {
        // Arrange
        string rootFolderPath = "C:\\";
        var fileSystemEntries = new List<string>()
        {
            rootFolderPath + "file1.txt",
            rootFolderPath + "file2.txt",
            rootFolderPath + "dir1\\file3.txt",
            rootFolderPath + "dir2\\file4.txt",
            rootFolderPath + "dir2\\dir3\\file5.txt",
            rootFolderPath + "dir2\\dir3\\file6.txt",
        };

        _directoryMock.Setup(x => x.Exists(rootFolderPath))
            .Returns(true);
        _directoryMock.Setup(x => x.EnumerateFileSystemEntries(rootFolderPath))
            .Returns(fileSystemEntries);

        var fileSystemVisitor = new FileSystemVisitor(_directoryMock.Object, rootFolderPath);

        // Act
        var entries = fileSystemVisitor.ToList();

        // Assert
        entries.Should().BeEquivalentTo(fileSystemEntries);
    }

    [Fact]
    public void GetEnumerator_ReturnsFilteredEntries_WhenFilterIsApplied()
    {
        // Arrange
        var rootDirectoryPath = "C:\\";
        var file1Path = $"{rootDirectoryPath}\\file1.txt";
        var file2Path = $"{rootDirectoryPath}\\file2.txt";
        var file3Path = $"{rootDirectoryPath}\\file3.jpg";
        var dir1Path = $"{rootDirectoryPath}\\dir1";
        var dir2Path = $"{rootDirectoryPath}\\dir2";
        var dir3Path = $"{rootDirectoryPath}\\dir3";
        var file4Path = $"{rootDirectoryPath}\\dir3\\file3.txt";
        _directoryMock.Setup(d => d.Exists(rootDirectoryPath)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(rootDirectoryPath))
            .Returns(new List<string> { file1Path, file2Path,file3Path, dir1Path, dir2Path, dir3Path });
        _directoryMock.Setup(d => d.Exists(dir3Path)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(dir3Path))
            .Returns(new List<string> { file4Path });
        var expectedEntries = new List<string> { file1Path, file2Path, file4Path };
        Func<string, bool> filter = path => path.EndsWith(".txt");
        var fileSystemVisitor = new FileSystemVisitor(_directoryMock.Object, rootDirectoryPath, filter);

        // Act
        var actualEntries = new List<string>();
        foreach (var entry in fileSystemVisitor)
        {
            actualEntries.Add(entry);
        }

        // Assert
        actualEntries.Should().BeEquivalentTo(expectedEntries);
        _directoryMock.Verify(d => d.Exists(rootDirectoryPath), Times.Once);
        _directoryMock.Verify(d => d.EnumerateFileSystemEntries(rootDirectoryPath), Times.Once);
        _directoryMock.Verify(d => d.EnumerateFileSystemEntries(file3Path), Times.Never);
        _directoryMock.Verify(d => d.EnumerateFileSystemEntries(dir3Path), Times.Once);
    }
    
    [Fact]
    public void StartEventHandler_ShouldBeInvoked_OnEnumeration()
    {
        // Arrange
        _directoryMock.Setup(x => x.Exists(It.IsAny<string>()))
            .Returns(true);
        var fileSystemVisitor = new FileSystemVisitor(_directoryMock.Object, "C:\\test");
        var eventRaised = false;
        fileSystemVisitor.OnStart += (sender, args) => { eventRaised = true; };

        // Act
        var result = fileSystemVisitor.GetEnumerator().MoveNext();

        // Assert
        eventRaised.Should().BeTrue();
    }
    
    [Fact]
    public void FinishEventHandler_ShouldBeInvoked_AfterEnumeration()
    {
        // Arrange
        _directoryMock.Setup(x => x.Exists(It.IsAny<string>()))
            .Returns(true);
        var rootDirectoryPath = "C:\\test";
        var fileSystemVisitor = new FileSystemVisitor(_directoryMock.Object, rootDirectoryPath);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(rootDirectoryPath))
            .Returns(new List<string> { $"{rootDirectoryPath}\\f1", $"{rootDirectoryPath}\\f2" });
        var eventRaised = false;
        fileSystemVisitor.OnFinish += (sender, args) => { eventRaised = true; };

        // Act
        fileSystemVisitor.GetEnumerator().MoveNext();
        var eventRaisedStateAfterFirstMove = eventRaised;
        fileSystemVisitor.ToList();

        // Assert
        eventRaisedStateAfterFirstMove.Should().BeFalse();
        eventRaised.Should().BeTrue();
    }
    
    [Fact]
    public void FileFoundEventHandler_ShouldBeInvoked_ForEveryFile()
    {
        // Arrange
        var rootDirectoryPath = "C:\\";
        var file1Path = $"{rootDirectoryPath}\\file1.txt";
        var file2Path = $"{rootDirectoryPath}\\file2.txt";
        var file3Path = $"{rootDirectoryPath}\\file3.jpg";
        var dir1Path = $"{rootDirectoryPath}\\dir1";
        var dir2Path = $"{rootDirectoryPath}\\dir2";
        var dir3Path = $"{rootDirectoryPath}\\dir3";
        var file4Path = $"{rootDirectoryPath}\\dir3\\file4.txt";
        _directoryMock.Setup(d => d.Exists(rootDirectoryPath)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(rootDirectoryPath))
            .Returns(new List<string> { file1Path, file2Path, file3Path, dir1Path, dir2Path, dir3Path });
        _directoryMock.Setup(d => d.Exists(dir1Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir2Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir3Path)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(dir3Path))
            .Returns(new List<string> { file4Path });
        var expectedEntries = new List<string> { file1Path, file2Path, file3Path, file4Path };
        var actualEntries = new List<string>();
        var fileSystemVisitor = new FileSystemVisitor(_directoryMock.Object, rootDirectoryPath);
        fileSystemVisitor.OnFileFound += (sender, e) =>
        {
            actualEntries.Add(e.Entry);
        };

        // Act
        fileSystemVisitor.ToList();

        // Assert
        actualEntries.Should().BeEquivalentTo(expectedEntries);
    }
    [Fact]
    public void DirectoryFoundEventHandler_ShouldBeInvoked_ForDirectory()
    {
        // Arrange
        var rootDirectoryPath = "C:\\";
        var file1Path = $"{rootDirectoryPath}\\file1.txt";
        var file2Path = $"{rootDirectoryPath}\\file2.txt";
        var file3Path = $"{rootDirectoryPath}\\file3.jpg";
        var dir1Path = $"{rootDirectoryPath}\\dir1";
        var dir2Path = $"{rootDirectoryPath}\\dir2";
        var dir3Path = $"{rootDirectoryPath}\\dir3";
        var file4Path = $"{rootDirectoryPath}\\dir3\\file4.txt";
        _directoryMock.Setup(d => d.Exists(rootDirectoryPath)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(rootDirectoryPath))
            .Returns(new List<string> { file1Path, file2Path, file3Path, dir1Path, dir2Path, dir3Path });
        _directoryMock.Setup(d => d.Exists(dir1Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir2Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir3Path)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(dir3Path))
            .Returns(new List<string> { file4Path });
        var expectedEntries = new List<string> { dir1Path, dir2Path, dir3Path };
        var actualEntries = new List<string>();
        var fileSystemVisitor = new FileSystemVisitor(_directoryMock.Object, rootDirectoryPath);
        fileSystemVisitor.OnDirectoryFound += (sender, e) =>
        {
            actualEntries.Add(e.Entry);
        };

        // Act
        fileSystemVisitor.ToList();

        // Assert
        actualEntries.Should().BeEquivalentTo(expectedEntries);
    }
    
    [Fact]
    public void FilteredFileFoundEventHandler_ShouldBeInvoked_ForFilteredFiles()
    {
        // Arrange
        var rootDirectoryPath = "C:\\";
        var file1Path = $"{rootDirectoryPath}\\file1.txt";
        var file2Path = $"{rootDirectoryPath}\\file2.txt";
        var file3Path = $"{rootDirectoryPath}\\file3.jpg";
        var dir1Path = $"{rootDirectoryPath}\\dir1";
        var dir2Path = $"{rootDirectoryPath}\\dir2";
        var dir3Path = $"{rootDirectoryPath}\\dir3";
        var file4Path = $"{rootDirectoryPath}\\dir3\\file4.txt";
        _directoryMock.Setup(d => d.Exists(rootDirectoryPath)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(rootDirectoryPath))
            .Returns(new List<string> { file1Path, file2Path, file3Path, dir1Path, dir2Path, dir3Path });
        _directoryMock.Setup(d => d.Exists(dir1Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir2Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir3Path)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(dir3Path))
            .Returns(new List<string> { file4Path });
        var expectedEntries = new List<string> { file3Path };
        var actualEntries = new List<string>();
        Func<string, bool> filter = x => x.EndsWith(".jpg");
        var fileSystemVisitor = new FileSystemVisitor(_directoryMock.Object, rootDirectoryPath, filter);
        fileSystemVisitor.OnFilteredFileFound += (sender, e) =>
        {
            actualEntries.Add(e.Entry);
        };

        // Act
        fileSystemVisitor.ToList();

        // Assert
        actualEntries.Should().BeEquivalentTo(expectedEntries);
    }
    
    [Fact]
    public void FilteredDirectoryFoundEventHandler_ShouldBeInvoked_ForFilteredDirectories()
    {
        // Arrange
        var rootDirectoryPath = "C:\\";
        var file1Path = $"{rootDirectoryPath}\\file1.txt";
        var file2Path = $"{rootDirectoryPath}\\file2.txt";
        var file3Path = $"{rootDirectoryPath}\\file3.jpg";
        var dir1Path = $"{rootDirectoryPath}\\dir1";
        var dir2Path = $"{rootDirectoryPath}\\dir2";
        var dir3Path = $"{rootDirectoryPath}\\dir3";
        var file4Path = $"{rootDirectoryPath}\\dir3\\file4.txt";
        _directoryMock.Setup(d => d.Exists(rootDirectoryPath)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(rootDirectoryPath))
            .Returns(new List<string> { file1Path, file2Path, file3Path, dir1Path, dir2Path, dir3Path });
        _directoryMock.Setup(d => d.Exists(dir1Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir2Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir3Path)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(dir3Path))
            .Returns(new List<string> { file4Path });
        var expectedEntries = new List<string> { dir3Path };
        var actualEntries = new List<string>();
        Func<string, bool> filter = x => x.EndsWith("dir3");
        var fileSystemVisitor = new FileSystemVisitor(_directoryMock.Object, rootDirectoryPath, filter);
        
        // Act
        fileSystemVisitor.OnFilteredDirectoryFound += (sender, e) =>
        {
            actualEntries.Add(e.Entry);
        };
        fileSystemVisitor.ToList();

        // Assert
        actualEntries.Should().BeEquivalentTo(expectedEntries);
    }
    
    [Fact]
    public void ToList_ShouldExcludeDirectories_WhenFlaggedAsExcludeInEventArgs()
    {
        // Arrange
        var rootDirectoryPath = "C:\\";
        var file1Path = $"{rootDirectoryPath}\\file1.txt";
        var file2Path = $"{rootDirectoryPath}\\file2.txt";
        var file3Path = $"{rootDirectoryPath}\\file3.jpg";
        var dir1Path = $"{rootDirectoryPath}\\dir1";
        var dir2Path = $"{rootDirectoryPath}\\dir2";
        var dir3Path = $"{rootDirectoryPath}\\dir3";
        var file4Path = $"{rootDirectoryPath}\\dir3\\file4.txt";
        _directoryMock.Setup(d => d.Exists(rootDirectoryPath)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(rootDirectoryPath))
            .Returns(new List<string> { file1Path, file2Path, file3Path, dir1Path, dir2Path, dir3Path });
        _directoryMock.Setup(d => d.Exists(dir1Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir2Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir3Path)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(dir3Path))
            .Returns(new List<string> { file4Path });
        var expectedEntries = new List<string> { file1Path, file2Path, file3Path, dir1Path, dir2Path };
        var fileSystemVisitor = new FileSystemVisitor(_directoryMock.Object, rootDirectoryPath);
        
        // Act
        fileSystemVisitor.OnDirectoryFound += (sender, e) =>
        {
            if (e.Entry.EndsWith("dir3"))
            {
                e.Exclude = true;
            }
        };
        var actualEntries = fileSystemVisitor.ToList();

        // Assert
        actualEntries.Should().BeEquivalentTo(expectedEntries);
    }
    [Fact]
    public void ToList_ShouldAbortEnumeration_WhenFlaggedAsAbortInEventArgs()
    {
        // Arrange
        var rootDirectoryPath = "C:\\";
        var file1Path = $"{rootDirectoryPath}\\file1.txt";
        var file2Path = $"{rootDirectoryPath}\\file2.txt";
        var file3Path = $"{rootDirectoryPath}\\file3.jpg";
        var dir1Path = $"{rootDirectoryPath}\\dir1";
        var dir2Path = $"{rootDirectoryPath}\\dir2";
        var dir3Path = $"{rootDirectoryPath}\\dir3";
        var file4Path = $"{rootDirectoryPath}\\dir3\\file4.txt";
        _directoryMock.Setup(d => d.Exists(rootDirectoryPath)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(rootDirectoryPath))
            .Returns(new List<string> { file1Path, file2Path, file3Path, dir1Path, dir2Path, dir3Path });
        _directoryMock.Setup(d => d.Exists(dir1Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir2Path)).Returns(true);
        _directoryMock.Setup(d => d.Exists(dir3Path)).Returns(true);
        _directoryMock.Setup(d => d.EnumerateFileSystemEntries(dir3Path))
            .Returns(new List<string> { file4Path });
        var expectedEntries = new List<string> { file1Path, file2Path };
        var fileSystemVisitor = new FileSystemVisitor(_directoryMock.Object, rootDirectoryPath);
        
        // Act
        fileSystemVisitor.OnFileFound += (sender, e) =>
        {
            if (e.Entry.EndsWith("file3.jpg"))
            {
                e.Abort = true;
            }
        };
        var actualEntries = fileSystemVisitor.ToList();

        // Assert
        actualEntries.Should().BeEquivalentTo(expectedEntries);
    }
}