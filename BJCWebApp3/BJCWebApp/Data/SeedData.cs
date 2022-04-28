using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BJCWebApp.Data
{
    // https://stackoverflow.com/questions/42471866/how-to-create-roles-in-asp-net-core-and-assign-them-to-users

    public static class Roles
    {
        public static readonly string Administrator = "Administrator";
        public static readonly string Basic = "Basic";
    }

    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                await EnsureRoleExists(serviceProvider, Roles.Administrator);
            }
        }

        private static async Task<IdentityResult> EnsureRoleExists(IServiceProvider serviceProvider,
                                                                      string role)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            IdentityResult IR = new IdentityResult();
            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            return IR;
        }
    }
}
