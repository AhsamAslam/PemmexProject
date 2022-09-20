using PemmexCommonLibs.Domain.Enums;

namespace Survey.API.Dto
{
    public class GenerateEmployeeSurveyDto
    {
        public List<EmployeeSurvey> employeeSurvey { get; set; }
        public int organizationSurveyId { get; set; }
    }
    public class EmployeeSurvey
    {
        public string employeeIdentifier { get; set; }
        public SurveyRate surveyRate { get; set; }
    }
}
