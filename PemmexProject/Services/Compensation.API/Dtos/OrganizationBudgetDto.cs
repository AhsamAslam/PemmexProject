using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Dtos
{
    public class OrganizationBudgetDto
    {
        public string businessIdentifier { get; set; }
        public double budgetPercentage { get; set; }
        public double budgetValue { get; set; }
        public double mandatoryBudgetPercentage { get; set; }
        public double mandatoryBudgetValue { get; set; }
        public double TotalbudgetValue { get; set; }
        public double TotalbudgetPercentageValue { get; set; }
        public double totalSalary { get; set; }
        public string jobFunction { get; set; }

    }
}
