using Holidays.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Database.context
{
    public class HolidaysContext : DbContext, IApplicationDbContext
    {
        public DbSet<EmployeeHolidays> EmployeeHolidays { get; set; }
        public DbSet<CompanyToEmployeeHolidays> CompanyToEmployeeHolidays { get; set; }
        public DbSet<HolidaySettings> HolidaySettings { get; set; }
        public DbSet<HolidayCalendar> HolidayCalendars { get; set; }
        private readonly IDateTime _dateTime;

        public HolidaysContext(
            DbContextOptions options,
            IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
                {
                    if (entry.State != EntityState.Unchanged) //ignore unchanged entities and history tables
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                entry.Entity.CreatedBy = "test";
                                entry.Entity.Created = _dateTime.Now;
                                break;

                            case EntityState.Modified:
                                entry.Entity.LastModifiedBy = "test";
                                entry.Entity.LastModified = _dateTime.Now;
                                break;
                        }

                    }
                }
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
