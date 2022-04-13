#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BJCWeb.Data;
using BJCWeb.Models;

namespace BJCWeb.Pages.CVFiles
{
    public class EditModel : PageModel
    {
        private readonly BJCWeb.Data.ApplicationDbContext _context;

        public EditModel(BJCWeb.Data.ApplicationDbContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(CVFile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CVFileExists(CVFile.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool CVFileExists(int id)
        {
            return _context.CVFile.Any(e => e.ID == id);
        }
    }
}
