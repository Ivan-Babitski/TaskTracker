using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace TaskTracker.UI.MVC.Models
{
    public class TaskViewModel
    {
        public bool IsOwner { get; set; }
        public bool IsJoined { get; set; }
        public int Id { get; set; }
        public string CreaterId { get; set; }
        public string NewExecutiorId { get; set; }
        public int? ParentTaskId { get; set; }
        public string Priority { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? ReOpenDate { get; set; }
        public DateTime? DueDate { get; set; }
        public IFormFile File { get; set; }
        public PriorityModel Priorities { get; set; }
        public List<TaskModel> ChildrenTasks { get; set; }
        public List<UserModel> Participants { get; set; }
        public List<SelectListItem> Friends { get; set; }
        public List<FileModel> Files { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}