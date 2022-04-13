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

namespace BJCWeb.Pages.CVFiles
{
    public class DeleteModel : PageModel
    {
        private readonly BJCWeb.Data.ApplicationDbContext _context;

        public DeleteModel(BJCWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public CVFile CVFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CVFile = await _context.CVFile.FirstOrDefaultAsync(m => m.ID == id);

            if (CVFile == null)
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

            CVFile = await _context.CVFile.FindAsync(id);

            if (CVFile != null)
            {
                _context.CVFile.Remove(CVFile);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
