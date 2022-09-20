using PemmexCommonLibs.Domain.Common;

namespace Survey.API.Dto
{
    public class OrganizationSurveyDto
    {
        public string organizationIdentifier { get; set; }
        public DateTime organizationSurveyDate { get; set; }
        public List<SurveyQuestion> SurveyQuestion { get; set; }

    }

    public class SurveyQuestion
    {
        public string segmentId { get; set; }
        public string segmentName { get; set; }
        public string questionId { get; set; }
        public string questionName { get; set; }
    }
}
