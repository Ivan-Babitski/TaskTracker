using System;
using System.Collections.Generic;

namespace TaskTracker.Core.Models
{
    public partial class Task
    {
        public Task()
        {
            File = new HashSet<File>();
            InverseParentTask = new HashSet<Task>();
            Processes = new HashSet<Process>();
            TasksTags = new HashSet<TaskTag>();
        }

        public int Id { get; set; }
        public string CreaterId { get; set; }
        public int? ParentTaskId { get; set; }
        public int PriorityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? ReOpenDate { get; set; }
        public DateTime? DueDate { get; set; }

        public virtual User Creater { get; set; }
        public virtual Task ParentTask { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual ICollection<File> File { get; set; }
        public virtual ICollection<Task> InverseParentTask { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
        public virtual ICollection<TaskTag> TasksTags { get; set; }
    }
}