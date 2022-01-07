using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Notifications.API.Database.Entities
{
    public class Notifications:AuditableEntity
    {
        [Key]
        public int notificationId { get; set; }
        public string EmployeeId { get; set; }
        public string title { get; set; }
        public bool isRead { get; set; }
        public TaskType tasks { get; set; }
        public string description { get; set; }
    }
}
