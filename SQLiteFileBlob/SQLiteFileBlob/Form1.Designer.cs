namespace SQLiteFileBlob
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonGetList = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUploadFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDownloadFile = new System.Windows.Forms.ToolStripButton();
            this.listViewFiles = new System.Windows.Forms.ListView();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonGetList,
            this.toolStripButtonUploadFile,
            this.toolStripButtonDownloadFile});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonGetList
            // 
            this.toolStripButtonGetList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonGetList.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonGetList.Image")));
            this.toolStripButtonGetList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonGetList.Name = "toolStripButtonGetList";
            this.toolStripButtonGetList.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonGetList.Text = "Get list";
            this.toolStripButtonGetList.Click += new System.EventHandler(this.toolStripButtonGetList_Click);
            // 
            // toolStripButtonUploadFile
            // 
            this.toolStripButtonUploadFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUploadFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUploadFile.Image")));
            this.toolStripButtonUploadFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUploadFile.Name = "toolStripButtonUploadFile";
            this.toolStripButtonUploadFile.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUploadFile.Text = "Upload file";
            this.toolStripButtonUploadFile.Click += new System.EventHandler(this.toolStripButtonUploadFile_Click);
            // 
            // toolStripButtonDownloadFile
            // 
            this.toolStripButtonDownloadFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDownloadFile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDownloadFile.Image")));
            this.toolStripButtonDownloadFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDownloadFile.Name = "toolStripButtonDownloadFile";
            this.toolStripButtonDownloadFile.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonDownloadFile.Text = "Download file";
            this.toolStripButtonDownloadFile.Click += new System.EventHandler(this.toolStripButtonDownloadFile_Click);
            // 
            // listViewFiles
            // 
            this.listViewFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFiles.Location = new System.Drawing.Point(0, 25);
            this.listViewFiles.Name = "listViewFiles";
            this.listViewFiles.Size = new System.Drawing.Size(800, 425);
            this.listViewFiles.TabIndex = 1;
            this.listViewFiles.UseCompatibleStateImageBehavior = false;
            this.listViewFiles.View = System.Windows.Forms.View.List;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listViewFiles);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "Save/Retrieve disk file to SQLite sample";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButtonGetList;
        private ToolStripButton toolStripButtonUploadFile;
        private ToolStripButton toolStripButtonDownloadFile;
        private ListView listViewFiles;
    }
}