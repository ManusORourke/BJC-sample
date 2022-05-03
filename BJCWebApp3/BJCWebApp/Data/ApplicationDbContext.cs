using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BJCWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BJCWebApp.Models.Job>? Job { get; set; }
        public DbSet<BJCWebApp.Models.CVFile>? CVFile { get; set; }
        public DbSet<BJCWebApp.Models.UserProfile>? UserProfile { get; set; }
        public DbSet<BJCWebApp.Models.JobApplication>? JobApplication { get; set; }
        public DbSet<BJCWebApp.Models.Location>? Location { get; set; }
    }
}