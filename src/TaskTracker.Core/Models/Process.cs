using System;

namespace TaskTracker.Core.Models
{
    public partial class Process
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string UserId { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? LeaveDate { get; set; }

        public virtual Task Task { get; set; }
        public virtual User User { get; set; }
    }
}
