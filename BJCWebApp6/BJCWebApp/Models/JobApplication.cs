namespace BJCWebApp.Models
{
    public class JobApplication
    {
        public JobApplication()
        {
            ID = 0;
            JobID = 0;
            UserProfileID = 0;
            IsInterested = true;
            DateApplied = DateTime.Now;
            //HasCV = false;
            CVFileID = null;
            FreeText = string.Empty;
        }

        public int ID { get; set; }

        public int JobID { get; set; }

        public int UserProfileID { get; set; }

        public string UserID { get; set; }

        public bool IsInterested { get; set; }

        public DateTime DateApplied { get; set; }

        public bool HasCV
        {
            get
            {
                return CVFileID > 0;
            }
        }

        public Nullable<int> CVFileID { get; set; }

        public string FreeText { get; set; }

        public Job? Job { get; set; }

        public UserProfile? UserProfile { get; set; }

        public CVFile? CVFile { get; set; }
    }
}
