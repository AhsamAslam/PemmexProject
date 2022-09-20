using PemmexCommonLibs.Domain.Common;

namespace Survey.API.Dto
{
    public class SegmentQuestionsWeightageDto : AuditableEntity
    {
        public int surveyQuestionId { get; set; }
        public Guid segmentId { get; set; }
        public string segmentName { get; set; }
        public Guid questionId { get; set; }
        public string questionContent { get; set; }

        public double surveyQuestionEngagement { get; set; }
        public double surveyQuestionNPS { get; set; }
        public double surveyQuestionAttrition { get; set; }
    }
}
