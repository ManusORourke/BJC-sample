using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace SQLiteFileBlob2
{
    public partial class CVFileView : UserControl
    {
        public CVFileView()
        {
            InitializeComponent();
        }

        // Get the existing list of files from DB

        private void toolStripButtonGetList_Click(object sender, EventArgs e)
        {
            GetFileList();
        }

        public void GetFileList()
        {
            using (var dbContext = new BJCContext())
            {
                DbSet<CVFile> files = dbContext.CVFiles;
                listViewFiles.Items.Clear();
                foreach (CVFile file in files)
                {
                    ListViewItem lvi = new ListViewItem(file.Title);
                    listViewFiles.Items.Add(lvi);
                }
            }
        }

        // Upload a CV to the DB

        private void toolStripButtonUploadFile_Click(object sender, EventArgs e)
        {
            Upload();
        }

        private void Upload()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Upload(dlg.FileName);
            }
        }

        int maxfilelen = 4 * 1024 * 1024;  // 4MB

        private void Upload(string filepath)
        {
            FileInfo info = new FileInfo(filepath);
            if (info.Exists)
            {
                if (info.Length < maxfilelen)
                {
                    byte[] bytes = new byte[info.Length];
                    FileStream stream = info.OpenRead();
                    stream.Read(bytes, 0, bytes.Length);
                    stream.Close();
                    CVFile file = new CVFile() { Title = info.Name, Content = bytes };
                    Upload(file);
                }
                else
                {
                    MessageBox.Show("Max file lenght is 4MB");
                }
            }
        }

        private void Upload(CVFile file)
        {
            using (var dbContext = new BJCContext())
            {
                var addedFile = dbContext.CVFiles.Add(file);
                dbContext.SaveChanges();
            }
        }

        // Retrive a file from the DB

        private void toolStripButtonDownloadFile_Click(object sender, EventArgs e)
        {
            Download();
        }

        private void Download()
        {
            if (listViewFiles.SelectedItems.Count > 0)
            {
                string tofind = listViewFiles.SelectedItems[0].Text;
                Download(tofind);
            }
        }

        private void Download(string title)
        {
            using (var dbContext = new BJCContext())
            {
                var file = dbContext.CVFiles.Where(a => a.Title == title).FirstOrDefault();
                if (file is not null)
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    dlg.FileName = file.Title;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Download(file, dlg.FileName);
                    }
                }
            }
        }

        private void Download(CVFile file, string filepath)
        {
            FileInfo info = new FileInfo(filepath);
            FileStream stream = info.OpenWrite();
            stream.Write(file.Content, 0, file.Content.Length);
            stream.Flush();
            stream.Close();
        }
    }
}
