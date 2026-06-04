using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Assignment.Repository.Seeding
{
    public static class IdentitySeeding 
    {
        public static async Task SeedAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if(!userManager.Users.Any())
            {
                var user = new AppUser()
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    IsDeleted = false,
                    CreatedAt = DateTime.Now
                };
                var result = await userManager.CreateAsync(user, "Admin@123");
                if(result.Succeeded)
                {
                    var role = new IdentityRole()
                    {
                        Name = "Admin"
                    };
                    await roleManager.CreateAsync(role);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

        }
    }
}
