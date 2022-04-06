using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SQLiteFileBlob
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

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("content")]
        public byte[] Content { get; set; }
    }
}
