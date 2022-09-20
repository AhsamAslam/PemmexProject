using Survey.API.Commands.CreateEmployeeSurvey.EmployeeSurveyRequests;
using Survey.API.Commands.SaveEmployeeSurvey;
using Survey.API.Database.Entities;
using Survey.API.Dto;

namespace Survey.API.Database.Interfaces
{
    public interface IEmployeeSurvey
    {
        Task<int> CreateEmployeeSurvey(List<GenerateEmployeeSurveyRequest> employeeSurveys);
        Task<IEnumerable<Entities.EmployeeSurvey>> GetEmployeeSurvey(string employeeIdentifier, string organizationIdentifier);

        Task<int> SaveEmployeeSurvey(List<UpdateEmployeeSurveyRequest>  updateEmployeeSurveyRequest);

        Task<IEnumerable<SurveySummaryDto>> GetSurveyAverage(List<string> employeeIdentifier);
        Task<IEnumerable<SurveySummaryDto>> GetOrganizationSurveyAverage(string organizationIdentifier);
        Task<IEnumerable<SurveyQuestionsDto>> GetQuestionnaire();
    }
}
