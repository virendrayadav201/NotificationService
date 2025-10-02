using AutoMapper;
using NotificationService.Application.DTOs;
using NotificationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.Mappers
{
    public class NotificationMapper: Profile
    {
        public NotificationMapper() {
            CreateMap<Notification, NotificationDTO>().ReverseMap();
        }
    }
}
