using TaskManager.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TaskManager.API.Database.context
{
    public interface IApplicationDbContext
    {
        DbSet<ChangeManager> ChangeManagers { get; set; }
        DbSet<ChangeHoliday> ChangeHolidays { get; set; }
        DbSet<ChangeCompensation> ChangeCompensations { get; set; }
        DbSet<ChangeTitle> ChangeTitles { get; set; }
        DbSet<ChangeGrade> ChangeGrades { get; set; }
        DbSet<BaseTask> BaseTasks { get; set; }
        DbSet<Notifications> Notifications { get; set; }
        DbSet<OrganizationApprovalSettings> organizationApprovalSettings { get; set; }
        DbSet<OrganizationApprovalSettingDetail> organizationApprovalSettingDetails { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
