using System.ComponentModel.DataAnnotations;

namespace BJCWebApp.Models
{
    public class Location
    {
        public Location()
        {
            Name = string.Empty;
        }
        public int ID { get; set; }
        [Display(Name = "Location")]
        public string Name { get; set; }
    }
}
