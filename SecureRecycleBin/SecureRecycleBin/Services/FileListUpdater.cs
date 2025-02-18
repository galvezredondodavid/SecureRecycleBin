using SecureRecycleBin.Utilities;

namespace SecureRecycleBin.Services;

public class FileListUpdater(ListView listView, string secureBinPath)
{
    public bool HasItems()
    {
        return Directory.Exists(secureBinPath) && Directory.EnumerateFileSystemEntries(secureBinPath).Any();
    }

    public void RefreshFileList()
    {
        if (listView.InvokeRequired)
        {
            listView.Invoke(RefreshFileList);
            return;
        }

        listView.BeginUpdate();
        listView.Items.Clear();
            
        try
        {
            AddFilesToList();
            AddDirectoriesToList();
        }
        finally
        {
            listView.EndUpdate();
        }
    }

    private void AddFilesToList()
    {
        foreach (var file in Directory.GetFiles(secureBinPath, "*", SearchOption.AllDirectories))
        {
            var fileInfo = new FileInfo(file);
            listView.Items.Add(new ListViewItem([fileInfo.Name, "File", FileUtilities.FormatSize(fileInfo.Length)]) { ImageKey = "File" });
        }
    }

    private void AddDirectoriesToList()
    {
        foreach (var dir in Directory.GetDirectories(secureBinPath, "*", SearchOption.AllDirectories))
        {
            listView.Items.Add(new ListViewItem([new DirectoryInfo(dir).Name, "Folder", "-"]) { ImageKey = "Folder" });
        }
    }
}