using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskTracker.UI.MVC.Interfaces;
using TaskTracker.UI.MVC.Models;

namespace TaskTracker.UI.MVC.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class TaskController : Controller
    {
        private ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public IActionResult UserPage()
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }
            var model = _taskService.GetAllTasks(UserId.Value);
            return View(model);
        }

        [HttpGet]
        public IActionResult CreateTask()
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }

            var model = _taskService.PrepareViewModel(UserId.Value);

            if (model == null)
            {
                return StatusCode(404);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult CreateTask(TaskCreateViewModel task)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }

            task.CreaterId = UserId.Value;
            var result = _taskService.CreateNewTask(task);

            if (result < 0)
            {
                return StatusCode(500);
            }
            
            return Redirect($"/task/{result}");
        }


        [HttpGet("task/{taskId}")]
        public IActionResult ViewTask(int taskId)
        {

            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }

            var model = _taskService.GetTaskById(taskId, UserId.Value);
            if (model == null)
            {
                return StatusCode(401);
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult RemoveExecutor(string userId, int taskId)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }

            if (!_taskService.RemoveExecutor(taskId, userId, UserId.Value))
            {
                return StatusCode(401);
            }

            return Redirect($"/task/{taskId}");
        }


        [HttpPost]
        public IActionResult AppointExecutor(TaskViewModel task)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }

            if (!_taskService.AppointExecutor(task, UserId.Value))
            {
                return StatusCode(401);
            }

            return Redirect($"/task/{task.Id}");
        }

        [HttpPost("/task/{taskId:int}/close")]
        public IActionResult CloseTask(int taskId)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }

            if (!_taskService.CloseTask(taskId, UserId.Value))
            {
                return StatusCode(401);
            }

            return Redirect($"/task/{taskId}");
        }   
        
        [HttpPost("/task/{taskId:int}/reopen")]
        public IActionResult ReOpenTask(int taskId)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }
            if (!_taskService.ReOpenTask(taskId, UserId.Value))
            {
                return StatusCode(401);
            }

            return Redirect($"/task/{taskId}");
        }     
        
        [HttpPost("/task/{taskId:int}/delete")]
        public IActionResult DeleteTask(int taskId)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }
            if (!_taskService.DeleteTask(taskId, UserId.Value))
            {
                return StatusCode(401);
            }

            return Redirect($"/task/userpage");
        }
    }
}