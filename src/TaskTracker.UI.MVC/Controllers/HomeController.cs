using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaskTracker.UI.MVC.Models;

namespace TaskTracker.UI.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [Route("~/")]
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return View();
            }

            return Redirect("/task/userpage");

        }   

            public IActionResult Logout()
            {
                if (User.Identity.IsAuthenticated)
                {
                    var cl = new HttpClient();
                    var response = cl.GetAsync("https://localhost:6004/account/logout");
                    return SignOut("Cookies", "oidc");
                }
                return Content("For logout needs login at the beginning!");
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        
    }
}