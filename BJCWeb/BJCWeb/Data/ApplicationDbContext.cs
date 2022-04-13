using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BJCWeb.Models;

namespace BJCWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<BJCWeb.Models.Job> Job { get; set; }
        public DbSet<BJCWeb.Models.CVFile> CVFile { get; set; }
        public DbSet<BJCWeb.Models.User> User { get; set; }
        public DbSet<BJCWeb.Models.JobApplication> JobApplication { get; set; }
    }
}