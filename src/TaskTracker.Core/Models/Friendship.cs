using System;

namespace TaskTracker.Core.Models
{
    public partial class Friendship
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime? LeaveDate { get; set; }

        public virtual User Friend { get; set; }
        public virtual User User { get; set; }
    }
}