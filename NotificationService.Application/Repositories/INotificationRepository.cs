using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Repositories
{
    public interface INotificationRepository
    {
        Notification GetNotification(int id);
        List<Notification> GetAllNotifications(int userId);
        bool DeleteNotification(int id);
        bool InsertNotification(Notification notification);
        bool UpdateNotification(Notification notification);
    }
}
