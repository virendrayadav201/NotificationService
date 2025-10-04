using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.DTOs
{
    public class NotificationDTO
    {
        public int NotificationId { get; set; }

        public int UserId { get; set; }

        public string Message { get; set; }

        public string Type { get; set; }

        public DateTime? SentAt { get; set; }

        public string Status { get; set; }
    }
}
