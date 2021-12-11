using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Dtos
{
    public class CompensationTask
    {
        public double AppliedSalary { get; set; }
        public double BaseSalary { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double NewAdditionalAgreedPart { get; set; }
        public double TotalMonthlyPay { get; set; }
        public double NewTotalMonthlyPay { get; set; }
        public double CarBenefit { get; set; }
        public double InsuranceBenefit { get; set; }
        public double PhoneBenefit { get; set; }
        public double EmissionBenefit { get; set; }
        public double HomeInternetBenefit { get; set; }
        public string EmployeeIdentifier { get; set; }


    }
}
