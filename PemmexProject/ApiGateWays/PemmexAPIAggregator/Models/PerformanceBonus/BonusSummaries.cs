using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Models
{
    public class JobBasedBonusSummary
    {
        public string jobFunction { get; set; }
        public string businessIdentifier { get; set; }
        public string businessName { get; set; }
        public double totalSalary { get; set; }
        public double bonusValue { get; set; }

    }
    public class BusinessUnitBonusSummary
    {
        public string title { get; set; }
        public string businessIdentifier { get; set; }
        public string businessName { get; set; }
        public double monthlySalaryValue { get; set; }
        public double bonusValue { get; set; }
    }

    public class PerformanceWholeTeamSummaryDto
    {
        public string title { get; set; }
        public string businessIdentifier { get; set; }
        public string businessName { get; set; }
        public double monthlySalaryValue { get; set; }
        public bool status { get; set; }
    }
}
