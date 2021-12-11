using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.NotificationHub
{
    public interface INotificationRepository
    {
        Task<int> SaveNotification(Notifications notifications, 
            CancellationToken cancellationToken);
        Task<List<Notifications>> GetAllNotification(string EmployeeId);
        Task<int> CountUnReadNotifications(string EmployeeId);
        Task<int> AddNotifications(Notifications notifications);
        Task UpdateNotificationToRead(string EmployeeId);
    }
}
