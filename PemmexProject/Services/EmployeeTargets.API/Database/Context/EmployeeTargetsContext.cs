using EmployeeTargets.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Interfaces;

namespace EmployeeTargets.API.Database.Context
{
    public class EmployeeTargetsContext : DbContext, IApplicationDbContext
    {
        private readonly IDateTime _dateTime;

        public EmployeeTargetsContext(DbContextOptions options,
            IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }
        public DbSet<HardTargets> HardTargets { get; set; }
        public DbSet<HardTargetsDetail> HardTargetsDetail { get; set; }
        public DbSet<SoftTargets> SoftTargets { get; set; }
        public DbSet<SoftTargetsDetail> SoftTargetsDetail { get; set; }
        public DbSet<PerfromanceBudgetPlanning> PerfromanceBudgetPlanning { get; set; }
        public DbSet<PerformanceEvaluationSummary> PerformanceEvaluationSummary { get; set; }
        public DbSet<PerformanceEvaluationSettings> PerformanceEvaluationSettings { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                return 0;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}
