using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using TaskTracker.UI.MVC.Interfaces;
using TaskTracker.UI.MVC.Models;

namespace TaskTracker.UI.MVC.Controllers
{
    [Authorize(Roles = "Administrotor, User")]
    public class FileController : Controller
    {
        private IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        public IActionResult SendFile(TaskViewModel filesOnly)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }

            if (filesOnly != null && filesOnly.File != null)
            {
                if (!_fileService.SendFile(filesOnly, UserId.Value))
                {
                    return StatusCode(404);
                }
            }
            else
            {
                return Redirect($"/task/userpage");
            }
            return Redirect($"/task/{filesOnly.Id}");
        }     
        
        [HttpGet("/file/getfile/{fileId:int}")]
        public IActionResult GetFile(int fileId)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }
            var result = _fileService.GetFile(fileId, UserId.Value);
            if (result == null)
            {
                return StatusCode(404);
            }
            
            return File(result.DataFiles, "application/octet-stream");
           // return File(result.DataFiles, result.ContentType);
        }

        [HttpDelete]
        public IActionResult DeleteFile(int fileId, int taskId)
        {
            Claim UserId = User.Claims.Where(t => t.Type == "UserId").FirstOrDefault();
            if (UserId == null)
            {
                return StatusCode(401);
            }
            _fileService.DeleteFile(fileId, UserId.Value);
            return Redirect($"/task/{taskId}");
        }
    }
}