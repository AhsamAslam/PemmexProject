namespace PemmexAPIAggregator.Models
{
    public class WholeTeamSummaryDto
    {
        public string managerIdentifier { get; set; }
        public string managerName { get; set; }
        public double teamSalary { get; set; }
        public double budgetAllocated { get; set; }
        public double budgetUsed { get; set; }
        public double budgetAllocatedPercentage { get; set; }
        public double budgetUsedPercentage { get; set; }
        public bool status { get; set; }
    }
}
