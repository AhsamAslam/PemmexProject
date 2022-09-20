using System;

namespace PemmexAPIAggregator.Models.PerformanceBonus
{
    public class PerfromanceBudgetPlanningDto
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public double companyProfitabilityMultiplier { get; set; }
        public DateTime bonusPayoutDate { get; set; }
        public string organizationIdentifier { get; set; }
    }
}
