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

namespace BJCWebApp.Controllers
{
    [Authorize]
    public class CVFilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CVFilesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CVFiles
        public async Task<IActionResult> Index()
        {
            return View(await _context.CVFile.ToListAsync());
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

            return File(cVFile.Content, MediaTypeNames.Application.Octet, cVFile.Title);
        }

        // GET: CVFiles/Create
        public IActionResult Create()
        {
            return View();
        }

        private readonly long _fileSizeLimit = 2097152;
        private readonly string[] _permittedExtensions = { ".docx", ".pdf", ".odt" };

        // POST: CVFiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserProfileId,Title,Content")] CVFile cVFile, IFormFile uploadFile)
        {
            if (!ModelState.IsValid)
            {
                return View(cVFile);
            }

            var formFileContent =
                await FileHelpers.ProcessFormFile<IFormFile>(
                    uploadFile, ModelState, _permittedExtensions,
                    _fileSizeLimit);

            if (ModelState.IsValid)
            {
                cVFile.Title = uploadFile.FileName;
                cVFile.UserProfileId = 1;
                cVFile.Content = formFileContent;
                _context.Add(cVFile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cVFile);
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserProfileId,Title,Content")] CVFile cVFile)
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
