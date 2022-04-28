using System.Text.Json.Serialization;

namespace BJCWebApp.Models
{
    public class JobApplication
    {
        public JobApplication()
        {
            ID = 0;
            JobID = 0;
            UserProfileID = 0;
            IsInterested = false;
            DateApplied = DateTime.Now;
            HasCV = false;
            CVFileID = null;
            FreeText = string.Empty;
        }

        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("jobid")]
        public int JobID { get; set; }

        [JsonPropertyName("clientid")]
        public int UserProfileID { get; set; }

        [JsonPropertyName("isinterested")]
        public bool IsInterested { get; set; }

        [JsonPropertyName("dateapplied")]
        public DateTime DateApplied { get; set; }

        [JsonPropertyName("havecv")]
        public bool HasCV { get; set; }

        [JsonPropertyName("cvid")]
        public Nullable<int> CVFileID { get; set; }

        [JsonPropertyName("freetext")]
        public string FreeText { get; set; }

        public Job Job { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}
