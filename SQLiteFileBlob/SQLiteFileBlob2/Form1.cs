using Microsoft.Data.Sqlite;

namespace SQLiteFileBlob2
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
    }
}