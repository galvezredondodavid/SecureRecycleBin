namespace SecureRecycleBin
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Button btnRandomize;
        private Button btnDelete;
        private ListView listViewFiles;
        private ColumnHeader colName;
        private ColumnHeader colType;
        private ColumnHeader colSize;
        private Label lblStatus;
        private FileSystemWatcher watcher;
        private ImageList fileIcons;

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnRandomize = new System.Windows.Forms.Button();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblStatus = new System.Windows.Forms.Label();
            this.watcher = new System.IO.FileSystemWatcher();
            this.fileIcons = new System.Windows.Forms.ImageList(this.components);
            this.btnDelete = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.watcher)).BeginInit();
            this.SuspendLayout();
            
            this.btnRandomize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRandomize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btnRandomize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRandomize.ForeColor = System.Drawing.Color.White;
            this.btnRandomize.Location = new System.Drawing.Point(623, 415);
            this.btnRandomize.Name = "btnRandomize";
            this.btnRandomize.Size = new System.Drawing.Size(165, 35);
            this.btnRandomize.TabIndex = 0;
            this.btnRandomize.Text = "Randomize Now";
            this.btnRandomize.UseVisualStyleBackColor = false;
            this.btnRandomize.Click += new System.EventHandler(this.BtnRandomize_Click);
            
            this.listViewFiles.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewFiles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.listViewFiles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colType,
            this.colSize});
            this.listViewFiles.ForeColor = System.Drawing.Color.White;
            this.listViewFiles.FullRowSelect = true;
            this.listViewFiles.HideSelection = false;
            this.listViewFiles.Location = new System.Drawing.Point(12, 12);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(776, 397);
            this.listViewFiles.TabIndex = 1;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.Details;
            this.listViewFiles.SmallImageList = this.fileIcons;
            
            this.colName.Text = "Name";
            this.colName.Width = 300;
            
            this.colType.Text = "Type";
            this.colType.Width = 150;
            
            this.colSize.Text = "Size";
            this.colSize.Width = 150;
            
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(12, 415);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(434, 35);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            
            this.watcher.EnableRaisingEvents = true;
            this.watcher.IncludeSubdirectories = true;
            this.watcher.NotifyFilter = ((System.IO.NotifyFilters)((System.IO.NotifyFilters.FileName | System.IO.NotifyFilters.DirectoryName)));
            this.watcher.SynchronizingObject = this;
            
            this.fileIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.fileIcons.ImageSize = new System.Drawing.Size(32, 32);
            this.fileIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.fileIcons.Images.Add("File", System.Drawing.SystemIcons.GetStockIcon(System.Drawing.StockIconId.DocumentNoAssociation));
            this.fileIcons.Images.Add("Folder", System.Drawing.SystemIcons.GetStockIcon(System.Drawing.StockIconId.Folder));
            
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnDelete.Enabled = false;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(452, 415);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(165, 35);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Secure Delete All";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            this.ClientSize = new System.Drawing.Size(800, 462);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.btnRandomize);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Form1";
            this.Text = "Secure Recycle Bin";
            ((System.ComponentModel.ISupportInitialize)(this.watcher)).EndInit();
            this.ResumeLayout(false);
        }
    }
}