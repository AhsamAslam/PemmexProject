using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTargets.API.Database.Entities
{
    public class Targets:AuditableEntity
    {
        [Key]
        public int TargetsId { get; set; }
        public string TargetsTitle { get; set; }
        public string TargetsDescription { get; set; }
        public EMeasurementCriteria MeasurementCriteria { get; set; }
        public EPerformanceCriteria PerformanceCriteria { get; set; }
        public ETargetsType TargetsType { get; set; }
        public double Weightage { get; set; }
        public DateTime TargetsDeadLineDate { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string OrganizationIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public bool isOrganizationBaseTarget { get; set; }
        public bool isBusinessBaseTarget { get; set; }
        public bool isBusinessUnitBaseTarget { get; set; }
        public bool isTeamBaseTarget { get; set; }
        public bool isActive { get; set; }
    }
}
