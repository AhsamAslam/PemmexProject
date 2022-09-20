using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Dtos;

namespace EmployeeTargets.API.Database.Interfaces
{
    public interface ISoftTargetsRepository
    {
        Task<int> CreateSoftTargets(SoftTargetsDto target);
        Task<IEnumerable<ShowSoftTargetsDto>> ShowSoftTargets(string employeeIdentifier);
        Task<IEnumerable<ShowSoftTargetsDto>> ShowSoftTargetsByUserId(string userId, string employeeIdentifier);
    }
}
