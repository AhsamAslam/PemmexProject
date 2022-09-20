namespace Survey.API.Dto
{
    public class SurveySettingDto
    {
        public List<SurveySegmentsDto> surveySegmentsDto { get; set; }
        public List<SurveyQuestionsDto> surveyQuestionsDtos { get; set; }
        public List<SegmentQuestionsWeightageDto> segmentQuestionsWeightageDtos { get; set; }
    }
}
