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
        public SaveResponse DeleteNotification(int id)
        {
            SaveResponse saveResponse = new SaveResponse();
            try
            {
                var result = _repository.DeleteNotification(id);
                if (result)
                {
                    AssignedSaveResponse(saveResponse, true, "Notification deleted successfully");
                }
                else
                {
                    AssignedSaveResponse(saveResponse,false,"Notification deletion failed!");
                }

            }
            catch (Exception ex) {
                AssignedSaveResponse(saveResponse, false, "Notification deletion failed!"+ ex.ToString());
            }
            return saveResponse;
            
        }

        private static void AssignedSaveResponse(SaveResponse saveResponse,bool isError, string? message)
        {
            saveResponse.IsErrored = isError;
            saveResponse.Message = message;
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

        public SaveResponse InsertNotification(NotificationDTO notification)
        {
            SaveResponse saveResponse = new SaveResponse();
            try
            {
                var result = _repository.InsertNotification(_mapper.Map<Notification>(notification));
                if (result)
                {
                    AssignedSaveResponse(saveResponse, true, "Notification inserted successfully");
                }
                else
                {
                    AssignedSaveResponse(saveResponse, false, "Notification insertion failed!");
                }

            }
            catch (Exception ex)
            {
                AssignedSaveResponse(saveResponse, false, "Notification insertion failed!" + ex.ToString());
            }
            return saveResponse;
        }

        public SaveResponse UpdateNotification(NotificationDTO notification)
        {
            SaveResponse saveResponse = new SaveResponse();
            try
            {
                var result = _repository.UpdateNotification(_mapper.Map<Notification>(notification));
                if (result)
                {
                    AssignedSaveResponse(saveResponse, true, "Notification updated successfully");
                }
                else
                {
                    AssignedSaveResponse(saveResponse, false, "Notification updation failed!");
                }

            }
            catch (Exception ex)
            {
                AssignedSaveResponse(saveResponse, false, "Notification updation failed!" + ex.ToString());
            }
            return saveResponse;
        }
    }
}
