using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using TaskTracker.IdentityServer.Interfaces;
using TaskTracker.IdentityServer.Models;

namespace TaskTracker.IdentityServer.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private IRoleService _roleService;
        private IUserService _userService;
        public AdminController(
            IRoleService roleService,
            IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserRoles()
        {
            var roles = _roleService.GetUserRolesAsync().Result;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            UserModel user = new UserModel { Roles = new List<Role>() };
            IList<Role> roles = await _roleService.GetAllRoles();
            user.Roles = roles;
            return View("AddUser", user);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(UserModel user)
        {
            user.Name = user.Email;
            await _userService.AddUserAsync(user);
            return Content("StatusCode: 201");
        }
        [HttpGet]
        public async Task<IActionResult> SetRole()
        {
            var roles = await _roleService.GetAllRoles();
            var users = _userService.GetUsers();
            UserModel model = new UserModel { Roles = roles, Users = users };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> SetRole(UserModel user)
        {
            await _roleService.AddRoleToUserAsync(user.Name, user.UserRole);
            return StatusCode(200);
        }
    }
}