using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Common.Dtos
{
    public class OrganizationUpdateEntity
    {
        public string EmployeeIdentifier { get; set; }
        public string Title { get; set; }
        public string Grade { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double BaseSalary { get; set; }
        public string ManagerIdentifier { get; set; }
        public string CostCenterIdentifier { get; set; }
        public double TotalMonthlyPay { get; set; }
        public double CarBenefit { get; set; }
        public double InsuranceBenefit { get; set; }
        public double PhoneBenefit { get; set; }
        public double EmissionBenefit { get; set; }
        public double HomeInternetBenefit { get; set; }
        public double one_time_bonus { get; set; }
        public DateTime EffectiveDate { get; set; }
        public TaskType TaskType { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
    }
}
