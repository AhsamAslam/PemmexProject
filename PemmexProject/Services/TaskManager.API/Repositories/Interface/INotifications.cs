using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Controllers;

namespace TaskManager.API.Repositories.Interface
{
    public interface INotifications
    {
        Task<IEnumerable<Notifications>> GetNotifications();
        Task<IEnumerable<Notifications>> GetNotificationsById(int Id);
        Task<Notifications> AddNotifications(Notifications Notifications);
        Task<Notifications> UpdateNotifications(Notifications Notifications);
        Task<int> DeleteNotifications(int Id);
    }
}
