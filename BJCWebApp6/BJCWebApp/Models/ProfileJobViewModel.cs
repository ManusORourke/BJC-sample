namespace BJCWebApp.Models
{
    public class ProfileJobViewModel
    {
        public UserProfile? UserProfile { get; set; }
        public int JobID { get; set; }
    }

    public class CVJobViewModel
    {
        public CVFile? CVFile{ get; set; }
        public int JobID { get; set; }
    }
}
