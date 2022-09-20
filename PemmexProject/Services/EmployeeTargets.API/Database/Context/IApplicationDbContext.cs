using EmployeeTargets.API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeTargets.API.Database.Context
{
    public interface IApplicationDbContext
    {
        DbSet<HardTargets> HardTargets { get; set; }
        DbSet<HardTargetsDetail> HardTargetsDetail { get; set; }
        DbSet<SoftTargets> SoftTargets { get; set; }
        DbSet<SoftTargetsDetail> SoftTargetsDetail { get; set; }
        DbSet<PerfromanceBudgetPlanning> PerfromanceBudgetPlanning { get; set; }
        DbSet<PerformanceEvaluationSummary> PerformanceEvaluationSummary { get; set; }
        DbSet<PerformanceEvaluationSettings> PerformanceEvaluationSettings { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
