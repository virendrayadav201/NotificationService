using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Application.DTOs;
using NotificationService.Application.Services.Abstractions;

namespace NotificationService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationServiceController : ControllerBase
    {
        private INotificationAppService _notificationAppService;
        public NotificationServiceController(INotificationAppService notificationAppService)
        {
            _notificationAppService= notificationAppService;   
        }

        [HttpGet]
        public IActionResult GetNotifications(int userId)
        {
            var result = _notificationAppService.GetNotifications(userId);
            if (result == null)
            {
                return NotFound("No notification found for this user");
            }
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetNotification(int id)
        {
            var result = _notificationAppService.GetNotification(id);
            if (result == null)
            {
                return NotFound("No notification found");
            }
            return Ok(result);
        }

        [HttpPost]
        public IActionResult InsertNotification([FromBody]NotificationDTO notification)
        {
           var result = _notificationAppService.InsertNotification(notification);
            if (!result)
                return BadRequest("Notification not inserted");
            return Ok("Notification inserted");
        }
        [HttpPost]
        public IActionResult DeleteNotification(int id)
        {
            var result = _notificationAppService.DeleteNotification(id);
            if (!result)
                return BadRequest("Notification not deleted");
            return Ok("Notification deleted");
        }
    }
}
