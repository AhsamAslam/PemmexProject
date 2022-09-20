using Compensation.API.Database.context;
using Compensation.API.Database.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Database.Context
{
    public class CompensationContext : DbContext, IApplicationDbContext
    {

        private readonly IDateTime _dateTime;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompensationContext(
            DbContextOptions options,
            IDateTime dateTime, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Entities.Compensation> Compensation { get; set; }
        public DbSet<CompensationSalaries> CompensationSalaries { get; set; }
        public DbSet<JobCatalogue> JobCatalogues { get; set; }
        public DbSet<Entities.OrganizationBudget> OrganizationBudgets { get; set; }
        public DbSet<Entities.FunctionalBudget> FunctionalBudgets { get; set; }
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
                                entry.Entity.CreatedBy = _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type == "sub")?.Value;
                                entry.Entity.Created = _dateTime.Now;
                                break;

                            case EntityState.Modified:
                                entry.Entity.LastModifiedBy = _httpContextAccessor.HttpContext.User.Claims.First(claim => claim.Type == "sub").Value;
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
