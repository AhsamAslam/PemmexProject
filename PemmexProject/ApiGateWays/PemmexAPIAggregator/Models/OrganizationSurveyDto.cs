using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;

namespace PemmexAPIAggregator.Models
{
    public class OrganizationSurveyDto : AuditableEntity
    {
        public int organizationSurveyId { get; set; }
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
