using PemmexCommonLibs.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Survey.API.Database.Entities
{
    public class SurveyQuestion : AuditableEntity
    {
        [Key]
        public int surveyQuestionId { get; set; }
        public Guid segmentId { get; set; }
        public string segmentName { get; set; }
        public Guid questionId { get; set; }
        public string questionName { get; set; }

        public double surveyQuestionEngagement { get; set; }
        public double surveyQuestionNPS { get; set; }
        public double surveyQuestionAttrition { get; set; }

        public virtual ICollection<OrganizationSurveyQuestion> OrganizationSurveyQuestion { get; set; }

    }
}
