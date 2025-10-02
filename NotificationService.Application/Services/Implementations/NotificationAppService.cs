using NotificationService.Application.DTOs;
using NotificationService.Application.Repositories;
using NotificationService.Application.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Services.Implementations
{
    public class NotificationAppService : INotificationAppService
    {
        private INotificationRepository _repository;
        public NotificationAppService(INotificationRepository repository)
        {
            _repository= repository;
        }
        public bool DeleteNotification(int id)
        {
            throw new NotImplementedException();
        }

        public NotificationDTO GetNotification(int id)
        {
            throw new NotImplementedException();
        }

        public List<NotificationDTO> GetNotifications(int userId)
        {
            throw new NotImplementedException();
        }

        public bool InsertNotification(NotificationDTO notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateNotification(NotificationDTO notification)
        {
            throw new NotImplementedException();
        }
    }
}
