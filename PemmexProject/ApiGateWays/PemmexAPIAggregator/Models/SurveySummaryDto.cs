namespace PemmexAPIAggregator.Models
{
    public class SurveySummaryDto
    {
        public int avgSurveyRate { get; set; }
        public string questionId { get; set; }
        public string questionName { get; set; }
        public string segmentId { get; set; }
        public string segmentName { get; set; }
    }
}
