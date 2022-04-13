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
    public class DeleteModel : PageModel
    {
        private readonly BJCWeb.Data.ApplicationDbContext _context;

        public DeleteModel(BJCWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public JobApplication JobApplication { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JobApplication = await _context.JobApplication.FirstOrDefaultAsync(m => m.ID == id);

            if (JobApplication == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JobApplication = await _context.JobApplication.FindAsync(id);

            if (JobApplication != null)
            {
                _context.JobApplication.Remove(JobApplication);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
