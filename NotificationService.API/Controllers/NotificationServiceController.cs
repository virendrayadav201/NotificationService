using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Admin")]
        [HttpGet("{userId}")]
        [ProducesDefaultResponseType(typeof(List<NotificationDTO>))]
        public IActionResult GetNotifications(int userId)
        {
            var result = _notificationAppService.GetNotifications(userId);
            if (result == null)
            {
                return NotFound("No notification found for this user");
            }
            return Ok(result);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesDefaultResponseType(typeof(NotificationDTO))]
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
        [ProducesDefaultResponseType(typeof(SaveResponse))]
        public IActionResult InsertNotification([FromBody]NotificationDTO notification)
        {
           var result = _notificationAppService.InsertNotification(notification);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesDefaultResponseType(typeof(SaveResponse))]
        public IActionResult DeleteNotification(int id)
        {
            var result = _notificationAppService.DeleteNotification(id);
            return Ok(result);
        }

        [HttpPut]
        [ProducesDefaultResponseType(typeof(SaveResponse))]
        public IActionResult UpdateNotification([FromBody]NotificationDTO notification)
        {
            var result = _notificationAppService.UpdateNotification(notification);
            return Ok(result);
        }
    }
}
