using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BJCWebApp.Models
{
    public class Job
    {
        public Job()
        {
            ID = 0;
            RefID = 0;
            Title = "";
            Company = "";
            Hours = "";
            ContractType = "";
            LocationID = 0;
            Salary = "";
            Description = "";
            Requirments = "";
            Benefits = "";
            Date = DateTime.Now;
            Archived = false;
            HideCompany = false;
        }
        
        [JsonPropertyName("id")]
        public int ID { get; set; }

        [JsonPropertyName("refid")]
        public int RefID { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("company")]
        public string Company { get; set; }

        [JsonPropertyName("hours")]
        public string Hours { get; set; }

        [JsonPropertyName("contracttype")]
        [Display(Name = "Contract Type")]
        public string ContractType { get; set; }

        [JsonPropertyName("location")]
        [Display(Name = "Location")]
        public int LocationID { get; set; }

        [JsonPropertyName("salary")]
        public string Salary { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("requirments")]
        public string Requirments { get; set; }

        [JsonPropertyName("benefits")]
        public string Benefits { get; set; }

        [JsonPropertyName("date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [JsonPropertyName("archived")]
        public bool Archived { get; set; }

        [JsonPropertyName("hidecompany")]
        public bool HideCompany { get; set; }

        public Location? Location { get; set; }
    }
}
