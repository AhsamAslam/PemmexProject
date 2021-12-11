using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;

namespace Organization.API.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Entities.Business> Businesses { get; set; }
        DbSet<Entities.Employee> Employees { get; set; }
        DbSet<Entities.CostCenter> CostCenters { get; set; }
        DbSet<Entities.EmployeeContacts> EmployeeContacts { get; set; }
        DbSet<Entities.EmployeeBonuses> Bonuses { get; set; }
        DbSet<spGetEmployeeTreeForManagerDto> SpGetEmployeeTreeForManagerDtos { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
