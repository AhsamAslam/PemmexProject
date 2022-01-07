using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Notifications.API.Database.context
{
    public class NotificationContext : DbContext, IApplicationDbContext
    {
        public DbSet<Entities.Notifications> Notifications { get; set; }
        

        private readonly IDateTime _dateTime;

        public NotificationContext(
            DbContextOptions options,
            IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken =default(CancellationToken))
        {
            try
            {
                var result = await base.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
           
    }
}
