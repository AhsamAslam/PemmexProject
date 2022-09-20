using PemmexCommonLibs.Domain.Common;

namespace Survey.API.Dto
{
    public class SurveySegmentsDto : AuditableEntity
    {
        public int surveySegmentsId { get; set; }
        public string surveySegments { get; set; }
    }
}
