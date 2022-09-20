using Notifications.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.API.NotificationHub
{
    public interface INotificationRepository
    {
        Task<int> SaveNotification(Database.Entities.Notifications notifications, 
            CancellationToken cancellationToken);

        Task<int> AddAnnounceNotification(List<AnnounceNotificationDto> notifications);
        Task<List<Database.Entities.Notifications>> GetAllNotification(string EmployeeId);
        Task<int> CountUnReadNotifications(string EmployeeId);
        Task<int> AddNotifications(Database.Entities.Notifications notifications);
        Task UpdateNotificationToRead(string EmployeeId);
    }
}
