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
    public class UserProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserProfilesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserProfiles
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole(Roles.Administrator))
            {
                return View(await _context.UserProfile.ToListAsync());
            }
            var user = await _userManager.GetUserAsync(User);
            var userProfiles = await _context.UserProfile
                .Where(m => m.UserID == user.Id).ToListAsync();

            if (userProfiles == null)
            {
                return View(new List<JobApplication>());
            }
            return View(userProfiles);
        }

        // GET: UserProfiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // GET: UserProfiles/Create
        public async Task<IActionResult> Create(int? jobid)
        {
            var user = await _userManager.GetUserAsync(User);
            UserProfile userProfile = new UserProfile();
            userProfile.UserID = user.Id;
            userProfile.Email = user.Email;
            ProfileJobViewModel profileJobVM = new ProfileJobViewModel() { UserProfile = userProfile };
            if (jobid != null) profileJobVM.JobID = (int)jobid;
            profileJobVM.UserProfile = userProfile;
            return View(profileJobVM);
        }

        // POST: UserProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PhoneNumber,Email,IsRegistered,ID,Gender,DateOfBirth,HighestLevelOfEducation,Location,PaymentType,UserID")] UserProfile userProfile, int? jobid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userProfile);
                await _context.SaveChangesAsync();
                if (jobid != null && jobid != 0)
                {
                    return RedirectToAction("Create", "JobApplications", new { jobid = jobid });
                }
                return RedirectToAction(nameof(Index));
            }
            ProfileJobViewModel profileJobVM = new ProfileJobViewModel() { UserProfile = userProfile };
            if (jobid != null) profileJobVM.JobID = (int)jobid;
            return View(profileJobVM);
        }

        // GET: UserProfiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile.FindAsync(id);
            if (userProfile == null)
            {
                return NotFound();
            }
            return View(userProfile);
        }

        // POST: UserProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,PhoneNumber,Email,IsRegistered,ID,Gender,DateOfBirth,HighestLevelOfEducation,Location,PaymentType,UserID")] UserProfile userProfile)
        {
            if (id != userProfile.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userProfile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserProfileExists(userProfile.ID))
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
            return View(userProfile);
        }

        // GET: UserProfiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userProfile = await _context.UserProfile
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // POST: UserProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userProfile = await _context.UserProfile.FindAsync(id);
            _context.UserProfile.Remove(userProfile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserProfileExists(int id)
        {
            return _context.UserProfile.Any(e => e.ID == id);
        }
    }
}
