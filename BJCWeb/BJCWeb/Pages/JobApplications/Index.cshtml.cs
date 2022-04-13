#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BJCWeb.Data;
using BJCWeb.Models;

namespace BJCWeb.Pages.JobApplications
{
    public class IndexModel : PageModel
    {
        private readonly BJCWeb.Data.ApplicationDbContext _context;

        public IndexModel(BJCWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<JobApplication> JobApplication { get;set; }

        public async Task OnGetAsync()
        {
            JobApplication = await _context.JobApplication.ToListAsync();
        }
    }
}
