using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTargets.API.Database.Entities
{
    public class HardTargets:AuditableEntity
    {
        [Key]
        public int HardTargetsId { get; set; }
        public string HardTargetsName { get; set; }
        public string HardTargetsDescription { get; set; }
        public EMeasurementCriteria MeasurementCriteria { get; set; }
        public double MeasurementCriteriaResult { get; set; }
        public double Weightage { get; set; }
        public string OrganizationIdentifier { get; set; }
        public DateTime EvaluationDateTime { get; set; }
        public bool isActive { get; set; }
        public TargetAssignType TargetType { get; set; }
    }
}
