using PemmexCommonLibs.Domain.Enums;
using System.Collections.Generic;

namespace Notifications.API.Dtos
{
    public class AnnounceNotificationDto
    {
        public int notificationId { get; set; }
        public string title { get; set; }
        public bool isRead { get; set; }
        public string description { get; set; }
        public TaskType tasks { get; set; }
        public string EmployeeId { get; set; }
    }
}
