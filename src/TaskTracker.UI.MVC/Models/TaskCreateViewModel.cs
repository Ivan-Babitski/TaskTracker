using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace TaskTracker.UI.MVC.Models
{
    public class TaskCreateViewModel
    {
        public string CreaterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public int PriorityId { get; set; }
        public int ParentTaskId { get; set; }
        public string Tags { get; set; }
        [DisplayName("Priority")]
        public List<SelectListItem> Priorities { get; set; }
        [DisplayName("Choose parent task")]
        public List<SelectListItem> Tasks { get; set; }
    }
}