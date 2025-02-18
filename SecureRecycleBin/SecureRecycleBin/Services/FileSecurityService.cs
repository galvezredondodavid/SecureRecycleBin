using System.Security.Cryptography;
using SecureRecycleBin.Utilities;

namespace SecureRecycleBin.Services;

public class FileSecurityService(string secureBinPath) : IDisposable
{
    private readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    public void SecureRandomization()
    {
        ProcessFiles();
        ProcessDirectories();
    }

    public void SecureDeleteAll()
    {
        DeleteFiles();
        DeleteDirectories();
    }

    private void ProcessFiles()
    {
        var files = Directory.GetFiles(secureBinPath, "*", SearchOption.AllDirectories);
        Parallel.ForEach(files, file =>
        {
            try
            {
                OverwriteFile(file);
                RandomizeFileName(file);
                FileUtilities.ScrubMetadata(file);
            }
            catch {/**/}
        });
    }

    private void ProcessDirectories()
    {
        var directories = Directory.GetDirectories(secureBinPath, "*", SearchOption.AllDirectories).OrderByDescending(d => d.Count(c => c == Path.DirectorySeparatorChar)).ToList();

        Parallel.ForEach(directories, dir =>
        {
            try
            {
                RandomizeDirectoryName(dir);
                FileUtilities.ScrubMetadata(dir);
            }
            catch {/**/}
        });
    }

    private void DeleteFiles()
    {
        var files = Directory.GetFiles(secureBinPath, "*", SearchOption.AllDirectories);
        Parallel.ForEach(files, file =>
        {
            try
            {
                OverwriteFile(file);
                File.Delete(file);
            }
            catch {/**/}
        });
    }

    private void DeleteDirectories()
    {
        var directories = Directory.GetDirectories(secureBinPath, "*", SearchOption.AllDirectories).OrderByDescending(d => d.Count(c => c == Path.DirectorySeparatorChar)).ToList();

        Parallel.ForEach(directories, dir =>
        {
            try
            {
                if (!IsRootDirectory(dir))
                {
                    Directory.Delete(dir, true);
                }
            }
            catch {/**/}
        });
    }

    private bool IsRootDirectory(string path)
    {
        return Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar).Equals(Path.GetFullPath(secureBinPath).TrimEnd(Path.DirectorySeparatorChar), StringComparison.OrdinalIgnoreCase);
    }

    private void OverwriteFile(string path)
    {
        const int bufferSize = 4096 * 16;
        var fileInfo = new FileInfo(path);

        if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
        {
            fileInfo.Attributes &= ~FileAttributes.ReadOnly;
        }

        using var fs = fileInfo.Open(FileMode.Open, FileAccess.Write, FileShare.None);
        var buffer = new byte[bufferSize];
        var bytesRemaining = fileInfo.Length;
            
        while (bytesRemaining > 0)
        {
            _rng.GetBytes(buffer);
            var bytesToWrite = (int)Math.Min(buffer.Length, bytesRemaining);
            fs.Write(buffer, 0, bytesToWrite);
            bytesRemaining -= bytesToWrite;
        }
        fs.Flush();
    }

    private void RandomizeFileName(string filePath)
    {
        var dir = Path.GetDirectoryName(filePath);
        var newName = $"{FileUtilities.GenerateRandomName(_rng, 12)}.random";
        if (dir == null) return;
        for (var i = 0; i < 3; i++)
        {
            try
            {
                File.Move(filePath, Path.Combine(dir, newName));
                return;
            }
            catch {/**/}
        }
    }

    private void RandomizeDirectoryName(string dirPath)
    {
        var parent = Path.GetDirectoryName(dirPath);
        var newName = FileUtilities.GenerateRandomName(_rng, 12);
        if (parent == null) return;

        for (var i = 0; i < 3; i++)
        {
            try
            {
                Directory.Move(dirPath, Path.Combine(parent, newName));
                return;
            }
            catch {/**/}
        }
    }

    public void Dispose()
    {
        _rng.Dispose();
        GC.SuppressFinalize(this);
    }
}