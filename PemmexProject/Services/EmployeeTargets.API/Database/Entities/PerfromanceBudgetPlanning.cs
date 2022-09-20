using PemmexCommonLibs.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTargets.API.Database.Entities
{
    public class PerfromanceBudgetPlanning : AuditableEntity
    {
        [Key]
        public int perfromanceBudgetPlanningId { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public double companyProfitabilityMultiplier { get; set; }
        public DateTime bonusPayoutDate { get; set; }
        public string organizationIdentifier { get; set; }
    }
}
