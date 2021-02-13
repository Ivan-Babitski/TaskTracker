using System;
using System.Collections.Generic;

namespace TaskTracker.Application.Models
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string CreaterId { get; set; }
        public int? ParentTaskId { get; set; }
        public int PriorityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? ReOpenDate { get; set; }
        public DateTime? DueDate { get; set; }
        IEnumerable<UserDto> Participants { get; set; }
    }
}