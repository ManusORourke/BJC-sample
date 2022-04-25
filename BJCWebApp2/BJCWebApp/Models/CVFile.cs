using System.Text.Json.Serialization;

namespace BJCWebApp.Models
{
    public class CVFile
    {
        public CVFile()
        {
            ID = 0;
            Title = "";
        }
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public byte[]? Content { get; set; }
    }
}

