using System.ComponentModel.DataAnnotations;

namespace BJCWebApp.Models
{
    public class CVFile
    {
        public CVFile()
        {
            ID = 0;
            Title = "";
        }
        public int ID { get; set; }

        public string UserID { get; set; }

        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Uploaded")]
        public DateTime DateUploaded { get; set; }

        public byte[]? Content { get; set; }

        public string Summary
        {
            get
            {
                return string.Format("{0}, {1}", Title, DateUploaded.ToShortDateString());
            }
        }
    }
}

