using PemmexCommonLibs.Domain.Enums;

namespace Survey.API.Database.Entities
{
    public class OrganizationSurveyQuestion
    {
        public int surveyQuestionId { get; set; }
        public int organizationSurveyId { get; set; }
        public SurveyRate? surveyRate { get; set; }
        public string surveyComments { get; set; }


        public SurveyQuestion SurveyQuestion { get; set; }
        public OrganizationSurvey OrganizationSurvey { get; set; }
    }
}
