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
using Microsoft.AspNetCore.Identity;

namespace BJCWebApp.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public JobsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Jobs
        public async Task<IActionResult> Index(string locationString, string searchString)
        {
            // JobLocationViewModel
            IQueryable<string> locationQuery = from m in _context.Job
                                               .Include(s => s.Location)
                                               orderby m.Location.Name
                                               select m.Location.Name;

            var jobs = from m in _context.Job
                       .Include(s => s.Location)
                       select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                //jobs = jobs.Where(s => s.Title!.Contains(searchString));
                // contains is case sensitive for SQLite, why?
                jobs = jobs.Where(s => EF.Functions.Like(s.Title, $"%{searchString}%"));
            }

            if (!string.IsNullOrEmpty(locationString))
            {
                jobs = jobs.Where(x => x.Location.Name == locationString);
            }
            var jobLocationVM = new JobLocationViewModel
            {
                Locations = new SelectList(await locationQuery.Distinct().ToListAsync()),
                Jobs = await jobs.ToListAsync()
            };
            return View(jobLocationVM);
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(s => s.Location)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            PopulateLocationsDropDownList();
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,RefID,Title,Company,Hours,ContractType,LocationID,Salary,Description,Requirments,Benefits,Date,Archived,HideCompany")] Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateLocationsDropDownList();
            return View(job);
        }

        // GET: Jobs/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            PopulateLocationsDropDownList(job.LocationID);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,RefID,Title,Company,Hours,ContractType,LocationID,Salary,Description,Requirments,Benefits,Date,Archived,HideCompany")] Job job)
        {
            if (id != job.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.ID))
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
            PopulateLocationsDropDownList();
            return View(job);
        }

        // GET: Jobs/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .FirstOrDefaultAsync(m => m.ID == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [Authorize(Roles = "Administrator")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Job.FindAsync(id);
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.ID == id);
        }
        
        private void PopulateLocationsDropDownList(object selectedLocation = null)
        {
            var locations = from d in _context.Location
                                   orderby d.ID
                                   select d;
            ViewBag.LocationID = new SelectList(locations.AsNoTracking(), nameof(Location.ID), nameof(Location.Name), selectedLocation);
        }

    }
}
