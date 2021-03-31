using Microsoft.AspNetCore.Identity;
using MKTFY.Models.Entities;
using System.Threading.Tasks;

namespace MKTFY.App.Seeds
{
    public static class UserAndRoleSeeder
    {
        public static async Task SeedUsersAndRoles(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            // Create the roles
            var memberRoleExists = await roleManager.RoleExistsAsync("member");
            if (!memberRoleExists)
                await roleManager.CreateAsync(new IdentityRole("member"));

            var adminRoleExists = await roleManager.RoleExistsAsync("administrator");
            if (!adminRoleExists)
                await roleManager.CreateAsync(new IdentityRole("administrator"));

            // Create the users
            var memberFound = await userManager.FindByNameAsync("tony.onyeka+member@launchpadbyvog.com");
            if (memberFound == null)
            {
                var user = new User
                {
                    UserName = "tony.onyeka+member@launchpadbyvog.com",
                    Email = "tony.onyeka+member@launchpadbyvog.com",
                    FirstName = "Tony",
                    LastName = "Member",
                };
                IdentityResult result = await userManager.CreateAsync(user, "Password1");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "member");
            }
            var adminFound = await userManager.FindByNameAsync("tony.onyeka+admin@launchpadbyvog.com");
            if (adminFound == null)
            {
                var user = new User
                {
                    UserName = "tony.onyeka+admin@launchpadbyvog.com",
                    Email = "tony.onyeka+admin@launchpadbyvog.com",
                    FirstName = "Tony",
                    LastName = "Admin",
                };
                IdentityResult result = await userManager.CreateAsync(user, "Password1");

                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, "administrator");
            }
        }
    }
}
