using NotificationService.Application.Repositories;
using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure.Persistance.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private NotificationsServiceDbContext _context;
        public NotificationRepository(NotificationsServiceDbContext context) { 
            _context = context;
        }
        public bool DeleteNotification(int id)
        {
            _context.Notifications.Remove(_context.Notifications.FirstOrDefault(n=>n.NotificationId == id));
            _context.SaveChanges();
            return true;
        }

        public List<Notification> GetAllNotifications(int userId)
        {
            return _context.Notifications.Where(n=>n.UserId == userId).ToList();
        }

        public Notification GetNotification(int id)
        {
            return _context.Notifications.FirstOrDefault(r => r.NotificationId == id); 
        }

        public bool InsertNotification(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateNotification(Notification notification)
        {
            _context.Notifications.Update(notification);
            _context.SaveChanges() ;
            return true;
        }
    }
}
