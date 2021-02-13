using System;
using System.Linq;
using TaskTracker.IdentityServer.Data;
using TaskTracker.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.Threading.Tasks;

namespace TaskTracker.IdentityServer
{
    public class SeedData
    {
        public static async Task EnsureSeedData(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var roleAdmin = "Administrator";
            var roleUser = "User";
            var roleModerator = "Moderator";

            var roleAdminExist = await roleManager.RoleExistsAsync(roleAdmin);
            var roleUserExist = await roleManager.RoleExistsAsync(roleUser);
            var roleModeratorExist = await roleManager.RoleExistsAsync(roleModerator);

            var userAdmin = "Admin";
            var userAdminResult = await userManager.FindByNameAsync(userAdmin);

            IdentityResult result;

            if (!roleAdminExist)
            {
                result = await roleManager.CreateAsync(new IdentityRole(roleAdmin));
                if (result.Succeeded)
                {
                    Log.Debug("role 'Administrator' created");
                    if (userAdminResult == null)
                    {
                        userAdminResult = new ApplicationUser()
                        {
                            UserName = "admin",
                            Email = "admin@admin",
                            EmailConfirmed = true
                        };
                        result = await userManager.CreateAsync(userAdminResult, "Aaaa!111");
                        if (result.Succeeded)
                        {
                            Log.Debug("user 'admin' created");
                            result = await userManager.AddToRoleAsync(userAdminResult, roleAdmin);
                            if (!result.Succeeded)
                            {
                                // todo: error processing
                                throw new Exception(result.Errors.First().Description);
                            }
                            else
                            {
                                Log.Debug("role 'Administrator' added to user 'admin'");
                            }
                        }
                    }
                    else
                    {
                        Log.Debug("user 'admin' already exists");
                    }
                }
            }
            else
            {
                Log.Debug("role 'Administrator' already exists");
            }
            if (!roleUserExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleUser));
                Log.Debug("role 'User' created");
            }
            else
            {
                Log.Debug("role 'User' already exists");
            }
            if (!roleModeratorExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleModerator));
                Log.Debug("role 'Moderator' created");
            }
            else
            {
                Log.Debug("role 'Moderator' already exists");
            }

        }
    }
}
