using AutoMapper;
using NotificationService.Application.DTOs;
using NotificationService.Application.Repositories;
using NotificationService.Application.Services.Abstractions;
using NotificationService.Domain.Entities;
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
        private IMapper _mapper;
        public NotificationAppService(INotificationRepository repository, IMapper mapper)
        {
            _repository= repository;
            _mapper= mapper;
        }
        public bool DeleteNotification(int id)
        {
            return _repository.DeleteNotification(id);
        }

        public NotificationDTO GetNotification(int id)
        {
            var result = _repository.GetNotification(id);
            return _mapper.Map<NotificationDTO>(result);
        }

        public List<NotificationDTO> GetNotifications(int userId)
        {
            return _mapper.Map<List<NotificationDTO>>(_repository.GetAllNotifications(userId));
        }

        public bool InsertNotification(NotificationDTO notification)
        {
            return _repository.InsertNotification(_mapper.Map<Notification>(notification));
        }

        public bool UpdateNotification(NotificationDTO notification)
        {
            return _repository.UpdateNotification(_mapper.Map<Notification>(notification));
        }
    }
}
