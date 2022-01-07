using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Dtos
{
    public class FunctionalBudgetDto
    {
        public string businessIdentifier { get; set; }
        public int budgetPercentage { get; set; }
        public double budgetValue { get; set; }
        public JobFunction jobFunction { get; set; }
    }
}
