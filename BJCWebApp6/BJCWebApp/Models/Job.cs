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
        
        public int ID { get; set; }

        public int RefID { get; set; }

        public string Title { get; set; }

        public string Company { get; set; }

        public string Hours { get; set; }

        [Display(Name = "Contract Type")]
        public string ContractType { get; set; }

        [Display(Name = "Location")]
        public int LocationID { get; set; }

        public string Salary { get; set; }

        public string Description { get; set; }

        public string Requirments { get; set; }

        public string Benefits { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public bool Archived { get; set; }

        public bool HideCompany { get; set; }

        public Location? Location { get; set; }
    }
}
