using PemmexCommonLibs.Domain.Enums;

namespace Survey.API.Commands.SaveEmployeeSurvey
{
    public class UpdateEmployeeSurveyRequest
    {
        public string employeeIdentifier { get; set; }
        public Guid segmentId { get; set; }

        public string segmentName { get; set; }

        public Guid questionId { get; set; }
        public string questionName { get; set; }
        public SurveyRate surveyRate { get; set; }
        public string? surveyComments { get; set; }
    }
}
