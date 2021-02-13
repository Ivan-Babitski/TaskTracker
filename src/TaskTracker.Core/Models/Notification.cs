namespace TaskTracker.Core.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string NotificationValue { get; set; }

        public virtual User User { get; set; }
    }
}
