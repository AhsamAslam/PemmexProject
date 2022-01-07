using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Models
{
    public class FunctionalBudgetSummary
    {
        public string JobFunction { get; set; }
        public string BusinessIdentifier { get; set; }
        public string BusinessName { get; set; }
        public double MonthlySalaryValue { get; set; }

    }
    public class BusinessUnitBudgetSummary
    {
        public string title { get; set; }
        public string BusinessIdentifier { get; set; }
        public double MonthlySalaryValue { get; set; }

    }
}
