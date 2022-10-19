using GatePass.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GatePass.Infrastructure.Data.Seeds;

public class IdentitySeed
{
    public static async Task SeedAsync(UserManager<AppUser> userManager,
          RoleManager<IdentityRole> roleManager,
          AppDbContext context)
    {
        if (!context.Locations.Any()) return;

        var agbps = await context.Locations.FirstOrDefaultAsync(p => p.Name == "AGBPS");
        var shillong = await context.Locations.FirstOrDefaultAsync(p => p.Name == "Shillong");

        if (!userManager.Users.Any())
        {
            var users = new List<AppUser> {
                    new AppUser
                    {
                        DisplayName = "Admin",
                        Email = "admin",
                        UserName = "admin",
                        PhoneNumber = "8976453420",
                        LocationId = agbps != null ? agbps.Id : Guid.Empty
                    },
                    new AppUser
                    {
                        DisplayName = "AGBPS Security",
                        Email = "agbps_sec",
                        UserName = "agbps_sec",
                        LocationId = agbps != null ? agbps.Id : Guid.Empty
                    },
                    new AppUser
                    {
                        DisplayName = "Shillong Security",
                        Email = "shillong_sec",
                        UserName = "shillong_sec",
                        LocationId = shillong != null ? shillong.Id : Guid.Empty
                    }
                };

            var roles = new List<IdentityRole>
                {
                    new IdentityRole { Name = "Admin"},
                    new IdentityRole { Name = "Member" }
                };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                await userManager.CreateAsync(user, "Pass@123");
                await userManager.AddToRoleAsync(user, "Member");

                if (user.UserName == "admin")
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
