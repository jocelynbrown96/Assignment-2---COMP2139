using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Assignment_1___COMP2139.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (!await roleManager.RoleExistsAsync("Organizer"))
                await roleManager.CreateAsync(new IdentityRole("Organizer"));

            if (!await roleManager.RoleExistsAsync("Attendee"))
                await roleManager.CreateAsync(new IdentityRole("Attendee"));
        }
    }
}