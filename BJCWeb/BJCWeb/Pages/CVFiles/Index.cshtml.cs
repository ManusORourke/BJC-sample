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
    public class IndexModel : PageModel
    {
        private readonly BJCWeb.Data.ApplicationDbContext _context;

        public IndexModel(BJCWeb.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CVFile> CVFile { get;set; }

        public async Task OnGetAsync()
        {
            CVFile = await _context.CVFile.ToListAsync();
        }
    }
}
