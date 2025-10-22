using NotificationService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Services.Abstractions
{
    public interface INotificationAppService
    {
        NotificationDTO GetNotification(int id);
        List<NotificationDTO> GetNotifications(int userId);
        SaveResponse DeleteNotification(int id);
        SaveResponse InsertNotification(NotificationDTO notification);
        SaveResponse UpdateNotification(NotificationDTO notification);
    }
}
