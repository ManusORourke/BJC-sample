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
    public class DetailsModel : PageModel
    {
        private readonly BJCWeb.Data.ApplicationDbContext _context;

        public DetailsModel(BJCWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
