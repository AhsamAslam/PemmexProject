using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;

namespace PemmexAPIAggregator.Models
{
    public class HardTargetsDto
    {
        public int HardTargetsId { get; set; }
        public string hardTargetsName { get; set; }
        public string hardTargetsDescription { get; set; }
        public EMeasurementCriteria measurementCriteria { get; set; }
        public double MeasurementCriteriaResult { get; set; }
        public double weightage { get; set; }
        //public string employeeIdentifier { get; set; }
        public string businessUnitIdentifier { get; set; }
        //public string teamIdentifier { get; set; }
        public DateTime evaluationDateTime { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public TargetAssignType TargetType { get; set; }
        //Custom

        [System.Text.Json.Serialization.JsonIgnore]
        public string organizationIdentifier { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public List<HardTargetsDetailDto> HardTargetsDetail { get; set; }

    }
    public class HardTargetsDetailDto
    {
        public string EmployeeIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public string CostCenterIdentifier { get; set; }
    }
}
