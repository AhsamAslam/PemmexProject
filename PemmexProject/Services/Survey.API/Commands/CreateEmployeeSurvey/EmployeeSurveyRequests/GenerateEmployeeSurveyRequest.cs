using PemmexCommonLibs.Domain.Enums;

namespace Survey.API.Commands.CreateEmployeeSurvey.EmployeeSurveyRequests
{
    public class GenerateEmployeeSurveyRequest
    {
        public string employeeIdentifier { get; set; }
        public string employeeName { get; set; }
        public string managerIdentifier { get; set; }
        public string managerName { get; set; }
        public string businessIdentifier { get; set; }
        public SurveyRate surveyRate { get; set; }
        public int organizationSurveyId { get; set; }
    }
}
