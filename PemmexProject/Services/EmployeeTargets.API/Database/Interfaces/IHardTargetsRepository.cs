using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Dtos;

namespace EmployeeTargets.API.Database.Interfaces
{
    public interface IHardTargetsRepository
    {
        Task<int> CreateHardTargets(HardTargetsDto target);
        Task<IEnumerable<ShowHardTargetsDto>> ShowHardTargets(string employeeIdentifier);
        Task<IEnumerable<ShowHardTargetsDto>> ShowHardTargetsByUserId(string userId, string employeeIdentifier);
        Task<int> UpdateHardTargetsMeasurementCriteriaResult(int Id, double MeasurementCriteriaResult);
        Task<int> UpdateHardTargetsWeightage(int Id, double Weightage);
    }
}
