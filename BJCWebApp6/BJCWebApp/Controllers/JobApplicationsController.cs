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
    [Authorize]
    public class JobApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public JobApplicationsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: JobApplications
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(Roles.Administrator))
            {
                return View(await _context.JobApplication
                    .Include(s => s.Job)
                    .Include(s => s.UserProfile)
                    .ToListAsync());
            }
            else
            {
                var user = await _userManager.GetUserAsync(User);
                    var applications = await _context.JobApplication
                    .Include(s => s.Job)
                    .Include(s => s.UserProfile)
                    .Where(a => a.UserID == user.Id).ToListAsync();
                return View(applications);
            }
        }

        // GET: JobApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var jobApplication = await _context.JobApplication
                .Include(s => s.Job)
                .Include(s => s.UserProfile)
                .Include(s => s.CVFile)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            if (!User.IsInRole(Roles.Administrator))
            {
                var user = await _userManager.GetUserAsync(User);
                if (jobApplication.UserID != user.Id)
                {
                    return Forbid();
                }
            }
            return View(jobApplication);
        }

        // GET: JobApplications/Create
        public async Task<IActionResult> Create(int? jobid)
        {
            if (jobid == null || jobid == 0)
            {
                return View(); // todo redirect somewhere else
            }
            var jobID = (int)jobid;
            var job = _context.Job
                .FirstOrDefault(m => m.ID == jobID);
            if (job == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var userProfiles = await _context.UserProfile
                .Where(m => m.UserID == user.Id).ToListAsync();
            if (userProfiles.Count == 0)
            {
                return RedirectToAction("Create", "UserProfiles", new { jobid = jobID } );
            }
            var cvFiles = await _context.CVFile
                .Where(m => m.UserID == user.Id).ToListAsync();
            if (cvFiles.Count == 0)
            {
                ViewBag.ShowLinkToUploadCV = true;
            }
            else
            {
                ViewBag.ShowLinkToUploadCV = false;
            }
            ViewBag.UserProfileID = new SelectList(userProfiles, nameof(UserProfile.ID), nameof(UserProfile.Summary));
            ViewBag.CVFileID = new SelectList(cvFiles, nameof(CVFile.ID), nameof(CVFile.Summary));
            ViewBag.Job = job;
            JobApplication application = new JobApplication()
            { JobID = jobID, DateApplied = DateTime.Now, IsInterested = true, UserID = user.Id };
            return View(application);
        }

        // POST: JobApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,JobID,UserProfileID,IsInterested,DateApplied,CVFileID,FreeText,UserID")] JobApplication jobApplication)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobApplication);
        }

        // GET: JobApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplication.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (jobApplication.UserID != user.Id)
            {
                return Forbid();
            }
            var job = _context.Job
                .FirstOrDefault(m => m.ID == jobApplication.JobID);
            var userProfiles = await _context.UserProfile
                .Where(m => m.UserID == user.Id).ToListAsync();
            var cvFiles = await _context.CVFile
                .Where(m => m.UserID == user.Id).ToListAsync();
            if (cvFiles.Count == 0)
            {
                ViewBag.ShowLinkToUploadCV = true;
            }
            else
            {
                ViewBag.ShowLinkToUploadCV = false;
            }
            ViewBag.UserProfileID = new SelectList(userProfiles, nameof(UserProfile.ID), nameof(UserProfile.Summary));
            ViewBag.CVFileID = new SelectList(cvFiles, nameof(CVFile.ID), nameof(CVFile.Summary));
            ViewBag.Job = job;
            return View(jobApplication);
        }

        // POST: JobApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,JobID,UserProfileID,IsInterested,DateApplied,CVFileID,FreeText")] JobApplication jobApplication)
        {
            if (id != jobApplication.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobApplicationExists(jobApplication.ID))
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
            return View(jobApplication);
        }

        // GET: JobApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplication
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            if (jobApplication.UserID != user.Id)
            {
                return Forbid();
            }
            return View(jobApplication);
        }

        // POST: JobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobApplication = await _context.JobApplication.FindAsync(id);
            _context.JobApplication.Remove(jobApplication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobApplicationExists(int id)
        {
            return _context.JobApplication.Any(e => e.ID == id);
        }
    }
}
