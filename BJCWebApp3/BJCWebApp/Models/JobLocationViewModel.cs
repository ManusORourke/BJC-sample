using Microsoft.AspNetCore.Mvc.Rendering;

namespace BJCWebApp.Models
{
    public class JobLocationViewModel
    {
        public List<Job>? Jobs { get; set; }
        public SelectList? Locations { get; set; }
        public string? LocationString { get; set; }
        public string? SearchString { get; set; }
    }
}
