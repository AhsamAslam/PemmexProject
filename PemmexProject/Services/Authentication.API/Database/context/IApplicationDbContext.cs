using Authentication.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Database.context
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Roles> Roles { get; set; }
        DbSet<Screens> Screens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
