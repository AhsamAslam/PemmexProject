using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Enumerations;

namespace TaskManager.API.Dtos
{
    public class NotificationDto
    {
        public int notificationId { get; set; }
        public string title { get; set; }
        public bool isRead { get; set; }
        public string description { get; set; }
        public TaskType tasks { get; set; }
        public string EmployeeId { get; set; }
    }
}
