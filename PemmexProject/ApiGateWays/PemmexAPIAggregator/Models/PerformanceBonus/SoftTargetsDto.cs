using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PemmexAPIAggregator.Models
{
    public class SoftTargetsDto
    {
        public int SoftTargetsId { get; set; }
        public string softTargetsName { get; set; }
        public string softTargetsDescription { get; set; }
        public EPerformanceCriteria performanceCriteria { get; set; }
        public string businessUnitIdentifier { get; set; }
        public DateTime evaluationDateTime { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public TargetAssignType TargetType { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public string organizationIdentifier { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<SoftTargetsDetailDto> SoftTargetsDetail { get; set; }


    }
    public class SoftTargetsDetailDto
    {
        public string businessIdentifier { get; set; }
        public string managerIdentifier { get; set; }
        public string costCenterIdentifier { get; set; }
        public string employeeIdentifier { get; set; }
    }
}
