using System.ComponentModel.DataAnnotations.Schema;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Organization.API.Database.Entities;
using Organization.API.Dtos;

namespace Organization.API.Database.Context
{
    public interface IApplicationDbContext
    {
        DbSet<Business> Businesses { get; set; }
        DbSet<Employee> Employees { get; set; }
        DbSet<CostCenter> CostCenters { get; set; }
        DbSet<EmployeeContacts> EmployeeContacts { get; set; }
        DbSet<EmployeeBonuses> Bonuses { get; set; }
        DbSet<sp_GetBusinessUnitsDto> sp_GetBusinessUnits { get; set; }
        DbSet<spGetEmployeeTreeForManagerDto> SpGetEmployeeTreeForManagerDtos { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
