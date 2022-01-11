using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.API.Database.Repositories.Interface
{
    public interface INotification
    {
        Task<int> SaveNotification(Database.Entities.Notifications notifications);
        Task<List<Database.Entities.Notifications>> GetAllNotification(string EmployeeId);
        Task<int> CountUnReadNotifications(string EmployeeId);
        Task<int> AddNotifications(Database.Entities.Notifications notifications);
        Task UpdateNotificationToRead(string EmployeeId);
    }
}
