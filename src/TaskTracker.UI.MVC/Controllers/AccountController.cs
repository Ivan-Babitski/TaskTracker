using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTracker.UI.MVC.Controllers
{
    public class AccountController : Controller
    {
        [Authorize]
        [Route("/account")]
        public IActionResult AccountProfile()
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }
            return View();
        }

        [HttpPost("/account/invitefriend/{friendName:length(3, 20)}")]
        public IActionResult InviteFriend(string friendName)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }
            string currentUserId = UserId.Value;


            //проверить если уже в друзьях
            //если нет
            //bool result = _accountService.InviveFriend(currentUserId ,friendName);
            //if(result)...
            //ViewBag.InviteResult = *****отправили или нет
            //можно на ViewBag отдать, а на вьюхе, если не NULL то MessageBox показывать с результатом

            return RedirectToAction("Index");
        }
        [HttpPost("/account/acceptfriend/{friendId:int}")]
        public IActionResult AcceptFriend(string acceptedFriendId)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }
            string currentUserId = UserId.Value;
            return RedirectToAction("Index");
        }

    }
}
