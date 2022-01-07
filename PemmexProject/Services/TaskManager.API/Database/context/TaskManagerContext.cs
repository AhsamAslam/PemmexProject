using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;

namespace TaskManager.API.Database.context
{
    public class TaskManagerContext : DbContext, IApplicationDbContext
    {
        public DbSet<ChangeManager> ChangeManagers { get; set; }
        public DbSet<ChangeHoliday> ChangeHolidays { get; set; }
        public DbSet<ChangeCompensation> ChangeCompensations { get; set; }
        public DbSet<ChangeTitle> ChangeTitles { get; set; }
        public DbSet<ChangeGrade> ChangeGrades { get; set; }
        public DbSet<BaseTask> BaseTasks { get; set; }
        public DbSet<BonusSettings> BonusSettings { get; set; }
        public DbSet<OrganizationApprovalSettings> organizationApprovalSettings { get; set; }
        public DbSet<OrganizationApprovalSettingDetail> organizationApprovalSettingDetails { get; set; }


        private readonly IDateTime _dateTime;

        public TaskManagerContext(
            DbContextOptions options,
            IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken =default(CancellationToken))
        {
            try
            {
                //foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
                //{
                //    if (entry.State != EntityState.Unchanged) //ignore unchanged entities and history tables
                //    {
                //        switch (entry.State)
                //        {
                //            case EntityState.Added:
                //                entry.Entity.CreatedBy = "test";
                //                entry.Entity.Created = _dateTime.Now;
                //                break;

                //            case EntityState.Modified:
                //                entry.Entity.LastModifiedBy = "test";
                //                entry.Entity.LastModified = _dateTime.Now;
                //                break;
                //        }

                //    }
                //}
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
