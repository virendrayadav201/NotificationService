using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.Application.Mappers;
using NotificationService.Application.Repositories;
using NotificationService.Application.Services.Abstractions;
using NotificationService.Application.Services.Implementations;
using NotificationService.Infrastructure.Persistance;
using NotificationService.Infrastructure.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Infrastructure
{
    public class ServiceRegistration
    {
        public static void RegisterService(IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(cnfg => cnfg.AddProfile<NotificationMapper>());
            services.AddScoped<INotificationAppService, NotificationAppService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddDbContext<NotificationsServiceDbContext>(option => option.UseSqlServer(config.GetConnectionString("DbConnection")));
        }
    }
}
