using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;

namespace Survey.API.Dto
{
    public class EmployeeSurveyDto
    {
        public string employeeIdentifier { get; set; }
        public string employeeName { get; set; }
        public string surveyComments { get; set; }
        public string managerIdentifier { get; set; }
        public string managerName { get; set; }
        public string businessIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
      
        public Guid segmentId { get; set; }
  
        public string segmentName { get; set; }
        
        public Guid questionId { get; set; }
        public string questionName { get; set; }
        public SurveyRate surveyRate { get; set; }
    }
}
