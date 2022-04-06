using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SearchUI
{
    public class Job
    {
        public Job()
        {
            ID = 0;
            Title = "";
            Company = "";
        }

        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("company")]
        public string Company { get; set; }
    }
}
