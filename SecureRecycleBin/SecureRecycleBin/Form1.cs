using SecureRecycleBin.Services;

namespace SecureRecycleBin;

public partial class Form1 : Form
{
    private readonly SecureBinManager _binManager;
    private readonly FileListUpdater _fileListUpdater;
    private readonly FileSecurityService _fileSecurityService;
    private bool _isProcessing;

    public Form1()
    {
        InitializeComponent();
        StartPosition = FormStartPosition.CenterScreen;
        _binManager = new SecureBinManager();
        _fileListUpdater = new FileListUpdater(listViewFiles, _binManager.SecureBinPath);
        _fileSecurityService = new FileSecurityService(_binManager.SecureBinPath);
            
        ConfigureWatcherEvents();
        SafeRefresh();
    }

    private void ConfigureWatcherEvents()
    {
        _binManager.Watcher.Created += (_, _) => SafeRefresh();
        _binManager.Watcher.Deleted += (_, _) => SafeRefresh();
        _binManager.Watcher.Renamed += (_, _) => SafeRefresh();
    }

    private async void BtnRandomize_Click(object sender, EventArgs e)
    {
        try
        {
            if (_isProcessing) return;

            _isProcessing = true;
            btnRandomize.Enabled = false;
            btnDelete.Enabled = false;
            _binManager.Watcher.EnableRaisingEvents = false;

            try
            {
                await Task.Run(() => _fileSecurityService.SecureRandomization());
                SafeRefresh();
                UpdateStatus("Secure randomization completed successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Randomization failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isProcessing = false;
                btnRandomize.Enabled = true;
                _binManager.EnsureBinExists();
                SafeRefresh();
                _binManager.Watcher.EnableRaisingEvents = true;
            }
        }
        catch {/**/}
    }

    private async void BtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            if (_isProcessing) return;

            if (MessageBox.Show("Permanently delete all files in the Secure Recycle Bin?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            _isProcessing = true;
            btnDelete.Enabled = false;
            btnRandomize.Enabled = false;
            _binManager.Watcher.EnableRaisingEvents = false;

            try
            {
                await Task.Run(() => _fileSecurityService.SecureDeleteAll());
                SafeRefresh();
                UpdateStatus("All files securely deleted");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Deletion failed: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isProcessing = false;
                _binManager.EnsureBinExists();
                SafeRefresh();
                btnRandomize.Enabled = true;
                _binManager.Watcher.EnableRaisingEvents = true;
            }
        }
        catch {/**/}
    }

    private void SafeRefresh()
    {
        _fileListUpdater.RefreshFileList();
        UpdateDeleteButtonState();
    }

    private void UpdateDeleteButtonState()
    {
        if (InvokeRequired)
        {
            Invoke(UpdateDeleteButtonState);
            return;
        }
        btnDelete.Enabled = _fileListUpdater.HasItems();
    }

    private void UpdateStatus(string message)
    {
        if (InvokeRequired) Invoke(new Action<string>(UpdateStatus), message);
        else lblStatus.Text = message;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _fileSecurityService.Dispose();
            _binManager.Watcher.Dispose();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }
}