namespace PemmexAPIAggregator.Models
{
    public class UpdateBonusFromPerformanceBonusRequest
    {
        public string employeeIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public double bonusAmount { get; set; }
    }
}
