using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Database.context
{
    public interface IApplicationDbContext
    {
        DbSet<Entities.Compensation> Compensation { get; set; }
        DbSet<Entities.CompensationBonuses> CompensationBonuses { get; set; }
        DbSet<Entities.CompensationSalaries> CompensationSalaries { get; set; }
        DbSet<Entities.JobCatalogue> JobCatalogues { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
