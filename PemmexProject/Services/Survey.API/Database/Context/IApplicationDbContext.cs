using Microsoft.EntityFrameworkCore;
using Survey.API.Database.Entities;

namespace Survey.API.Database.Context
{
    public interface IApplicationDbContext
    {
        DbSet<SurveyQuestion> SurveyQuestion { get; set; }
        DbSet<OrganizationSurvey> OrganizationSurvey { get; set; }
        DbSet<EmployeeSurvey> EmployeeSurvey { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
