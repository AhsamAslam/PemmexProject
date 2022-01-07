using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Database.context
{
    public interface IApplicationDbContext
    {
        DbSet<Entities.Compensation> Compensation { get; set; }
        DbSet<Entities.CompensationSalaries> CompensationSalaries { get; set; }
        DbSet<Entities.JobCatalogue> JobCatalogues { get; set; }
        DbSet<Entities.OrganizationBudget> OrganizationBudgets { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
