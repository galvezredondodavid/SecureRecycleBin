using System.Security.Cryptography;

namespace SecureRecycleBin.Utilities;

public static class FileUtilities
{
    public static string GenerateRandomName(RandomNumberGenerator rng, int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var bytes = new byte[length];
        rng.GetBytes(bytes);
        return new string(bytes.Select(b => chars[b % chars.Length]).ToArray());
    }

    public static string FormatSize(long bytes)
    {
        string[] sizes = ["B", "KB", "MB", "GB"];
        var order = 0;
        while (bytes >= 1024 && order < sizes.Length - 1)
        {
            order++;
            bytes /= 1024;
        }
        return $"{bytes:0.##} {sizes[order]}";
    }

    public static void ScrubMetadata(string path)
    {
        try
        {
            var randomDate = DateTime.Now.AddYears(-RandomNumberGenerator.GetInt32(1, 30));

            if (File.Exists(path))
            {
                File.SetCreationTime(path, randomDate);
                File.SetLastWriteTime(path, randomDate);
                File.SetLastAccessTime(path, randomDate);
                File.SetAttributes(path, FileAttributes.Normal);
            }
            else
            {
                Directory.SetCreationTime(path, randomDate);
                Directory.SetLastWriteTime(path, randomDate);
                Directory.SetLastAccessTime(path, randomDate);
            }
        }
        catch {/**/}
    }
}