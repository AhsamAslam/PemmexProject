using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTargets.API.Database.Entities
{
    public class SoftTargets : AuditableEntity
    {
        [Key]
        public int SoftTargetsId { get; set; }
        public string SoftTargetsName { get; set; }
        public string SoftTargetsDescription { get; set; }
        public EPerformanceCriteria PerformanceCriteria { get; set; }
        public string OrganizationIdentifier { get; set; }
        public DateTime EvaluationDateTime { get; set; }
        public bool isActive { get; set; }
        public TargetAssignType TargetType { get; set; }
    }
}
