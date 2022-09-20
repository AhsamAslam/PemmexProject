using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Options;
using Organization.API.Database.Context;
using Organization.API.Database.Entities;
using Organization.API.Dtos;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common;

namespace Organization.API
{
    public class PemmexOrganizationContext:DbContext,IApplicationDbContext
    {
        private readonly IDateTime _dateTime;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PemmexOrganizationContext(
            DbContextOptions options,
            IDateTime dateTime, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeContacts> EmployeeContacts { get; set; }
        public DbSet<spGetEmployeeTreeForManagerDto> SpGetEmployeeTreeForManagerDtos { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<EmployeeBonuses> Bonuses { get; set; }
        public DbSet<sp_GetBusinessUnitsDto> sp_GetBusinessUnits { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
                {
                    if (entry.State != EntityState.Unchanged && !entry.Entity.GetType().Name.Contains("_History")) //ignore unchanged entities and history tables
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

                        //add original values to history table (skip if this entity is not yet created)                 
                        if (entry.State == EntityState.Modified && entry.Entity.GetType().BaseType != null)
                        {
                            //check the base type exists (actually the derived type e.g. Record)
                            string entityBaseType = entry.Entity.GetType().Name;
                            if (string.IsNullOrEmpty(entityBaseType))
                                continue;

                            //check there is a history type for this entity type
                            Type entityHistoryType = Type.GetType("Organization.API.Entities." + entityBaseType + "_History");
                            if (entityHistoryType == null)
                                continue;

                            //create history object from the original values
                            var history = Activator.CreateInstance(entityHistoryType);
                            foreach (PropertyInfo property in entityHistoryType.GetProperties().Where(p => p.CanWrite && entry.OriginalValues.Properties.Any(x => x.Name == p.Name)))
                                property.SetValue(history, entry.OriginalValues[property.Name], null);

                            //add the history object to the appropriate DbSet
                            MethodInfo method = typeof(PemmexOrganizationContext).GetMethod("AddToDbSet");
                            MethodInfo generic = method.MakeGenericMethod(entityHistoryType);
                            generic.Invoke(this, new[] { history });
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
        public void AddToDbSet<T>(T value) where T : class
        {
            PropertyInfo property = GetType().GetProperties().FirstOrDefault(p => p.PropertyType.IsGenericType
                && p.PropertyType.Name.StartsWith("DbSet")
                && p.PropertyType.GetGenericArguments().Length > 0
                && p.PropertyType.GetGenericArguments()[0] == typeof(T));
            if (property == null)
                return;

            ((DbSet<T>)property.GetValue(this, null)).Add(value);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Business>()
                .HasMany(e => e.Employees);
            builder.Entity<Employee>()
            .Ignore(i => i.ManagerName)
            .Ignore(i => i.BusinessName)
            .Ignore(i => i.CostCenterName)
            .Ignore(i => i.CostCenterIdentifier)
            .Ignore(i => i.BusinessIdentifier);
            builder.Entity<spGetEmployeeTreeForManagerDto>().HasNoKey().ToView(null);
            builder.Entity<sp_GetBusinessUnitsDto>().HasNoKey().ToView(null);

        }
    }
}
