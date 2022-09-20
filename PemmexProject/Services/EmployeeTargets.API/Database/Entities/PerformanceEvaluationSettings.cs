using PemmexCommonLibs.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTargets.API.Database.Entities
{
    public class PerformanceEvaluationSettings : AuditableEntity
    {
        [Key]
        public int performanceEvaluationSettingsId { get; set; }
        public double organizationIdentifier { get; set; }
        public double minimumPercentage { get; set; }
        public double targetPercentage { get; set; }
        public double maximumPercentage { get; set; }
        public bool isActive { get; set; }
    }
}
