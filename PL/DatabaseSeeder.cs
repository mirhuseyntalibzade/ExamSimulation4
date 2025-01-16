using DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PL
{
    public static class DatabaseSeeder
    {
        public static async Task SeedData(IServiceProvider provider)
        {
            var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            var db = provider.GetRequiredService<AppDbContext>();
            
            db.Database.Migrate();

            string[] roles = ["Admin", "User"];
            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            string email = "admin@gmail.com";
            string userName = "admin";
            IdentityUser? user = await userManager.FindByEmailAsync(email);
            if (user is null)
            {
                IdentityUser admin = new IdentityUser
                {
                    Email =email,
                    UserName = userName
                };
                IdentityResult result = await userManager.CreateAsync(admin, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "Admin");
                }
            }

        }
    }
}
