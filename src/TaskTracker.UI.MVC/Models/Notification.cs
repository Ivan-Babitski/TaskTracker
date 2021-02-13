using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTracker.UI.MVC.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string NotificationValue { get; set; }
    }
}