using EmployeeTargets.API.Database.Entities;

namespace EmployeeTargets.API.Database.Interfaces
{
    public interface IPerformanceBudgetRepository
    {
        Task<int> CreatePerfromanceBudgetPlanning(PerfromanceBudgetPlanning perfromanceBudgetPlanning);
        Task<PerfromanceBudgetPlanning> GetCreatePerfromanceBudgetPlanning(string organizationIdentifier);

        Task<int> CreatePerformanceEvaluationSummary(List<PerformanceEvaluationSummary> performanceEvaluationSummaries);
        Task<IEnumerable<PerformanceEvaluationSummary>> GetPerformanceEvaluationSummary(string organizationIdentifier);
        Task<IEnumerable<PerformanceEvaluationSummary>> GetPerformanceEvaluationSummaryDetail(string[] employeeIdentifier);


        Task<int> CreatePerformanceEvaluationSetting(PerformanceEvaluationSettings performanceEvaluationSettings);
        Task<int> DeletePerformanceEvaluationSetting(int Id);
        Task<IEnumerable<PerformanceEvaluationSettings>> ShowPerformanceEvaluationSetting(string organizationIdentifier);
        Task<double> GetBonusAmountByEmployeeIdentifier(string employeeIdentifier);
        Task<int> UpdatePerformanceEvaluationSummaryIsActive(string[] employeeIdentifier);
    }
}
