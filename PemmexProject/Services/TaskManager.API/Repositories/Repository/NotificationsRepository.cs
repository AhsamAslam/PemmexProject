using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Controllers;
using TaskManager.API.Repositories.Interface;

namespace TaskManager.API.Repositories.Repository
{
    public class NotificationsRepository : INotifications
    {
        public Task<Notifications> AddNotifications(Notifications Notifications)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteNotifications(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notifications>> GetNotifications()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Notifications>> GetNotificationsById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<Notifications> UpdateNotifications(Notifications Notifications)
        {
            throw new NotImplementedException();
        }
    }
}
