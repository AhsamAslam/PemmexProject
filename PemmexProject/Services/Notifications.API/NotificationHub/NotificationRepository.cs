using Microsoft.EntityFrameworkCore;
using Notifications.API.Database.context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.API.NotificationHub
{
    public class NotificationRepository :INotificationRepository
    {
        private readonly IApplicationDbContext _context;
        public NotificationRepository(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddNotifications(Database.Entities.Notifications notifications)
        {
            try
            {
                await _context.Notifications.AddAsync(notifications);
                await _context.SaveChangesAsync();
                return notifications.notificationId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> CountUnReadNotifications(string EmployeeId)
        {
            try
            {
                var n = await _context.Notifications
                    .Where(n => n.EmployeeId == EmployeeId && n.isRead == false)
                    .ToListAsync();
                return n.Count();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Database.Entities.Notifications>> GetAllNotification(string EmployeeId)
        {
            try
            {
                return await _context.Notifications.Where(n => n.EmployeeId == EmployeeId)
                    .ToListAsync();
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task<int> SaveNotification(Database.Entities.Notifications notifications, CancellationToken cancellationToken)
        {
            try
            {
                _context.Notifications.Add(notifications);
                await _context.SaveChangesAsync(cancellationToken);
                return notifications.notificationId;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public async Task UpdateNotificationToRead(string Id)
        {
            try
            {
                var n = await _context.Notifications
                    .Where(n => n.EmployeeId == Id).ToListAsync();
                n.ForEach(n => n.isRead = true);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
