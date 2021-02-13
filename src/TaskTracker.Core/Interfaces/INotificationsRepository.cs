using System.Collections.Generic;
using TaskTracker.Core.Models;

namespace TaskTracker.Core.Interfaces
{
    public interface INotificationsRepository
    {
        IEnumerable<Notification> GetNotifications();
        Notification GetNotificationById(int notificationId);
        int Create(Notification notification);
    }
}