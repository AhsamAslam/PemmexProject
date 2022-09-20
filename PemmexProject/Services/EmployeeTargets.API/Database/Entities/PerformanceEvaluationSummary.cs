using PemmexCommonLibs.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace EmployeeTargets.API.Database.Entities
{
    public class PerformanceEvaluationSummary : AuditableEntity
    {
        [Key]
        public int performanceEvaluationSummaryId { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string country { get; set; }
        public string grade { get; set; }
        public string jobFunction { get; set; }
        public double totalSalary { get; set; }
        public double bonusPercentage { get; set; }
        public double bonusAmount { get; set; }
        public double resultedBonusPercentage { get; set; }
        public double resultedBonusAmountBeforeMultiplier { get; set; }
        public double performanceMultiplier { get; set; }
        public double resultedBonusAmountAfterMultiplier { get; set; }
        public double companyProfitabilityMultiplier { get; set; }
        public double finalBonusAmount { get; set; }
        public DateTime bonusPayoutDate { get; set; }
        public string employeeIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string managerIdentifier { get; set; }
        public bool IsActive { get; set; }
    }
}
