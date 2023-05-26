using EmptyStock.Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmptyStock.Mvc;

public static class SetupIdentityExtension
{
    public static async Task<WebApplication> SetupIdentity(this WebApplication application)
    {
        using (var scope = application.Services.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<StockUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
            // check every role
            foreach (var role in IdentityHelpers.Roles)
            {
                if (await roleManager.RoleExistsAsync(role))
                    continue;

                await roleManager.CreateAsync(new IdentityRole<int> { Name = role });
            }
            // check administrator
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Email == "admin@admin.admin");
            if (user is null)
            {
                var role = new IdentityRole<int>
                {
                    Name = "admin"
                };

                await roleManager.CreateAsync(role);
                var admin = new StockUser
                {
                    Email = "admin@admin.admin",
                    UserName = "admin@admin.admin",
                };
                var result = await userManager.CreateAsync(admin, "Admin111!");

                var roleResult = await userManager.AddToRoleAsync(admin, "admin");
            }
        }
        return application;
    }
}
