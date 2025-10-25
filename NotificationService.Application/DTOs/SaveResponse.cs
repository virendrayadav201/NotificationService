using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationService.Application.DTOs
{
    public class SaveResponse
    {
        public bool IsError { get; set; }
        public string? Message { get; set; }

    }
}
