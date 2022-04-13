#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BJCWeb.Data;
using BJCWeb.Models;

namespace BJCWeb.Pages.JobApplications
{
    public class CreateModel : PageModel
    {
        private readonly BJCWeb.Data.ApplicationDbContext _context;

        public CreateModel(BJCWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public JobApplication JobApplication { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JobApplication.Add(JobApplication);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
