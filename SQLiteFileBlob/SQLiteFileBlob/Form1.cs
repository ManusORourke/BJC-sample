using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace SQLiteFileBlob
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeDB();
        }

        // Create database if it doesn't exist

        private string dbpath = "test.db3";

        private void InitializeDB()
        {
            if (!File.Exists(dbpath))
            {
                string cmd = "CREATE TABLE CVFiles(ID INTEGER PRIMARY KEY AUTOINCREMENT, Title NVarChar, Content BLOB)";
                using (var connection = new SqliteConnection("Data Source=" + dbpath))
                using (var command = new SqliteCommand(cmd, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
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