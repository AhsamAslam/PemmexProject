using EmployeeTargets.API.Database.Entities;

namespace EmployeeTargets.API.Database.Interfaces
{
    public interface ITargetsRepository
    {
        Task<int> CreateSoftTargets(Targets target);
        Task<IEnumerable<Targets>> ShowSoftTargets();
    }
}
