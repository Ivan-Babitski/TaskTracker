using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TaskTracker.IdentityServer.Interfaces;
using TaskTracker.IdentityServer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTracker.IdentityServer.Data;
using System.Data;
using System;

namespace TaskTracker.IdentityServer.Services
{
    public class UserManagerService : IUserService
    {
        private const string AdminRole = "Admin";
        private const string UserRole = "User";
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IRoleService _roleService;
        private TaskContext _taskContext;

        public UserManagerService(
            UserManager<ApplicationUser> userService,
            IHttpContextAccessor httpContext,
            IRoleService roleService,
            TaskContext taskContext)
        {
            _httpContext = httpContext;
            _userManager = userService;
            _roleService = roleService;
            _taskContext = taskContext;
        }

        public async Task AddUserAsync(UserModel user)
        {
            //checking mached roles
            
            bool roleCheck = false;
            foreach (var item in user.Roles)
            {
                if(item.isActive)
                {
                    roleCheck = true;
                    break;
                }
            }
            //user check++
            var currentUser = await _userManager.GetUserAsync(_httpContext.HttpContext.User);
            if (await _userManager.IsInRoleAsync(currentUser, AdminRole) && roleCheck)
            {
                var result = await _userManager.FindByNameAsync(user.Name);

                if (result == null)
                {
                    result = await _userManager.FindByEmailAsync(user.Email);
                }

                if (result == null)
                {
                    result = new ApplicationUser
                    {
                        UserName = user.Name,
                        Email = user.Email,
                        EmailConfirmed = true
                    };
                    var create = await _userManager.CreateAsync(result, user.Password);
                    if (create.Succeeded)
                    {
                        foreach (var item in user.Roles)
                        {
                            if (item.isActive)
                            {
                                await _roleService.AddRoleToUserAsync(user.Name, item.Name);
                            }
                        }
                    }
                }
            }
        }

        public async Task AddUserAsync(RegistrationViewModel user)
        {
            var result = await _userManager.FindByNameAsync(user.Name);

            if (result == null)
            {
                result = await _userManager.FindByEmailAsync(user.Email);
            }

            if (result == null)
            {
                result = new ApplicationUser
                {
                    UserName = user.Name,
                    Email = user.Email,
                    EmailConfirmed = true
                };
                var create = await _userManager.CreateAsync(result, user.Password);
                if (create.Succeeded)
                {
                    await _roleService.AddRoleToUserAsync(user.Name, UserRole);
                    CreateTaskTrackerUser(result);
                }
            }
        }

        public List<UserModel> GetUsers()
        {
            var users = _userManager.Users;
            List<UserModel> usernames = new List<UserModel>();
            foreach (var item in users)
            {
                usernames.Add(new UserModel { Name = item.UserName });
            }
            return usernames;
        }

        private void CreateTaskTrackerUser(ApplicationUser newUser)
        {
            try
            {
                _taskContext.StatusSupport();
                var command = _taskContext.CreateCommand();
                command.Parameters.Add("@Id", SqlDbType.NVarChar);
                command.Parameters["@Id"].Value = newUser.Id;
                command.Parameters.Add("@Login", SqlDbType.NVarChar);
                command.Parameters["@Login"].Value = newUser.UserName;
                command.Parameters.Add("@Name", SqlDbType.NVarChar);
                command.Parameters["@Name"].Value = newUser.UserName;
                command.Parameters.Add("@Productivity", SqlDbType.Float);
                command.Parameters["@Productivity"].Value = 1;
                command.CommandType = CommandType.Text;
                command.CommandText = "insert into Users(Id, Login, Name, Productivity) values(@Id, @Login, @Name, @Productivity)";
                var result = command.ExecuteScalar();
                _taskContext.SaveChanges();
            }
            catch (Exception e)
            {
                //user hasn't been created
            }
        }
    }
}