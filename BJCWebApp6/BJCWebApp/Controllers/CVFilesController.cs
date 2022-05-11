#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BJCWebApp.Data;
using BJCWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using BJCWebApp.Utilities;
using System.Net.Mime;
using Microsoft.AspNetCore.Identity;

namespace BJCWebApp.Controllers
{
    [Authorize]
    public class CVFilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CVFilesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: CVFiles
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(Roles.Administrator))
            {
                return View(await _context.CVFile.ToListAsync());
            }
            var user = await _userManager.GetUserAsync(User);
            var cvFiles = await _context.CVFile
                .Where(a => a.UserID == user.Id)
                .ToListAsync();
            return View(cvFiles);
        }

        // GET: CVFiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var cVFile = await _context.CVFile
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cVFile == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            bool isOwner = cVFile.UserID == user.Id ? true : false;
            if (User.IsInRole(Roles.Administrator) || isOwner)
            {
                return File(cVFile.Content, MediaTypeNames.Application.Octet, cVFile.Title);
            }
            return Forbid();
        }

        // GET: CVFiles/Create
        public async Task<IActionResult> Create(int? jobid)
        {
            var user = await _userManager.GetUserAsync(User);
            CVFile file = new CVFile() { UserID = user.Id };
            CVJobViewModel cVJobVM = new CVJobViewModel() { CVFile = file };
            if (jobid != null) cVJobVM.JobID = (int)jobid;
            return View(cVJobVM);
        }

        private readonly long _fileSizeLimit = 2097152;
        private readonly string[] _permittedExtensions = { ".docx", ".pdf", ".odt" };

        // POST: CVFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,Title,Content")] CVFile cVFile, IFormFile uploadFile, int? jobid)
        {
            CVJobViewModel cVJobVM = new CVJobViewModel() { CVFile = cVFile };
            if (jobid != null) cVJobVM.JobID = (int)jobid;
            if (ModelState.IsValid)
            {
                var formFileContent =
                await FileHelpers.ProcessFormFile<IFormFile>(
                    uploadFile, ModelState, _permittedExtensions,
                    _fileSizeLimit);

                var user = await _userManager.GetUserAsync(User);
                cVFile.Title = uploadFile.FileName;
                cVFile.UserID = user.Id;
                cVFile.Content = formFileContent;
                _context.Add(cVFile);
                await _context.SaveChangesAsync();
                if (jobid != null && jobid != 0)
                {
                    return RedirectToAction("Create", "JobApplications", new { jobid = (int)jobid });
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cVJobVM);
        }

        // GET: CVFiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cVFile = await _context.CVFile.FindAsync(id);
            if (cVFile == null)
            {
                return NotFound();
            }
            return View(cVFile);
        }

        // POST: CVFiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,Title,Content")] CVFile cVFile)
        {
            if (id != cVFile.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cVFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CVFileExists(cVFile.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cVFile);
        }

        // GET: CVFiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cVFile = await _context.CVFile
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cVFile == null)
            {
                return NotFound();
            }

            return View(cVFile);
        }

        // POST: CVFiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cVFile = await _context.CVFile.FindAsync(id);
            _context.CVFile.Remove(cVFile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CVFileExists(int id)
        {
            return _context.CVFile.Any(e => e.ID == id);
        }

    }
}
