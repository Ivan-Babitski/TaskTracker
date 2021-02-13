using System.Collections.Generic;

namespace TaskTracker.Core.Models
{
    public partial class User
    {
        public User()
        {
            FriendshipsFriend = new HashSet<Friendship>();
            FriendshipsUser = new HashSet<Friendship>();
            Notifications = new HashSet<Notification>();
            Processes = new HashSet<Process>();
            Task = new HashSet<Task>();
            UsersSpecialties = new HashSet<UserSpecialty>();
        }

        public string Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public double Productivity { get; set; }

        public virtual ICollection<Friendship> FriendshipsFriend { get; set; }
        public virtual ICollection<Friendship> FriendshipsUser { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
        public virtual ICollection<Task> Task { get; set; }
        public virtual ICollection<UserSpecialty> UsersSpecialties { get; set; }
    }
}
