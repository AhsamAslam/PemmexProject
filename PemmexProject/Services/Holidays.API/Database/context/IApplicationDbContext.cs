using Holidays.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Database.context
{
    public interface IApplicationDbContext
    {
        DbSet<EmployeeHolidays> EmployeeHolidays { get; set; }
        DbSet<CompanyToEmployeeHolidays> CompanyToEmployeeHolidays { get; set; }
        DbSet<HolidaySettings> HolidaySettings { get; set; }
        DbSet<HolidayCalendar> HolidayCalendars { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
