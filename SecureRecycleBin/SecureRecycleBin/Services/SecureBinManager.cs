namespace SecureRecycleBin.Services;

public class SecureBinManager
{
    public string SecureBinPath { get; }
    public FileSystemWatcher Watcher { get; } = new();

    public SecureBinManager()
    {
        const string secureFolderName = "Secure Recycle Bin";
        SecureBinPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), secureFolderName);
        Initialize();
    }

    private void Initialize()
    {
        EnsureBinExists();
        ConfigureWatcher();
    }

    private void ConfigureWatcher()
    {
        Watcher.Path = SecureBinPath;
        Watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName;
        Watcher.IncludeSubdirectories = true;
        Watcher.EnableRaisingEvents = true;
    }

    public void EnsureBinExists()
    {
        if (!Directory.Exists(SecureBinPath))
        {
            Directory.CreateDirectory(SecureBinPath);
        }
    }
}