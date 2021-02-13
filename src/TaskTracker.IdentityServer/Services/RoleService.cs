using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TaskTracker.IdentityServer.Interfaces;
using TaskTracker.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskTracker.IdentityServer.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public RoleService(IHttpContextAccessor httpContext, 
                           RoleManager<IdentityRole> roleManager, 
                           UserManager<ApplicationUser> userManager)
        {
            _httpContext = httpContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task AddRoleToUserAsync(string username, string role)
        {
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(role))
            {
                var result = await _userManager.FindByNameAsync(username);
                if (result != null)
                {
                    var roles = await _userManager.GetRolesAsync(result);
                    if (!roles.Contains(role))
                    {
                        var result2 = await _userManager.AddToRoleAsync(result, role);
                        if (result2.Succeeded)
                        {
                            //log
                        }
                        else
                        {
                            //log
                        }
                    }
                }
            }
        }

        public async Task<IList<Role>> GetAllRoles()
        {
            IList<Role> result = new List<Role>();
            var roles = _roleManager.Roles;
            foreach (var role in roles)
            {
                result.Add(new Role { Name = await _roleManager.GetRoleNameAsync(role) });
            }

            return result;
        }

        public async Task<IList<string>> GetUserRolesAsync()
        {
            var currentUser = _httpContext.HttpContext.User;
            var currentUserName = _userManager.GetUserName(currentUser);
            if (currentUserName != null)
            {
                var user = await _userManager.FindByNameAsync(currentUserName);
                if (user != null)
                {
                    return await _userManager.GetRolesAsync(user);
                }
            }
            return default;
        }

        private async Task<IdentityUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByNameAsync(email);
        }
    }
}