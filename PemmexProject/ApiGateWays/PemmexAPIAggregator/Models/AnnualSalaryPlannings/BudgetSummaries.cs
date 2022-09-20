using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Models
{
    public class OrganizationalBudgetSummary
    {
        public string jobFunction { get; set; }
        public string businessIdentifier { get; set; }
        public string businessName { get; set; }
        public double totalSalary { get; set; }
        public double budgetPercentage { get; set; }
        public double budgetValue { get; set; }
        public double mandatoryBudgetValue { get; set; }
        public double mandatoryBudgetPercentage { get; set; }
        public double TotalbudgetValue { get; set; }
        public double TotalbudgetPercentageValue { get; set; }

    }
    public class BusinessUnitBudgetSummary
    {
        public string title { get; set; }
        public string businessIdentifier { get; set; }
        public string businessName { get; set; }
        public double monthlySalaryValue { get; set; }
        public double budgetValue { get; set; }
        public string BUnitEmployeeIdentifier { get; set; }
    }
    
}
