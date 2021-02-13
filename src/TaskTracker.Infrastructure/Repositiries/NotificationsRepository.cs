using System.Collections.Generic;
using System.Linq;
using TaskTracker.Core.Interfaces;
using TaskTracker.Core.Models;

namespace TaskTracker.Infrastructure.Data.Repositories
{
    public class NotificationsRepository : INotificationsRepository
    {
        private readonly TaskTrackerContext _context;
        public NotificationsRepository(TaskTrackerContext context)
        {
            _context = context;
        }

        public IEnumerable<Notification> GetNotifications()
        {
            foreach (var item in _context.Notifications)
            {
                yield return item;
            }
        }

        public Notification GetNotificationById(int notificationId)
        {
            if (notificationId < 0)
            {
                throw new System.ArgumentOutOfRangeException();
            }

            return _context.Notifications.FirstOrDefault(p => p.Id == notificationId);
        }

        public int Create(Notification notification)
        {
            if (notification is null)
            {
                throw new System.ArgumentNullException();
            }

            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return notification.Id;
        }
    }
}