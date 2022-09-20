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
        public double IncrementPercentage { get; set; }
        public DateTime EffectiveDate { get; set; }
        public TaskType TaskType { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
    }
    public class BudgetPromotionUpdateEntity
    {
        public Guid Emp_Guid { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CurrentTitle { get; set; }
        public string NewTitle { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string OrganizationCountry { get; set; }
        public string CurrentGrade { get; set; }
        public string NewGrade { get; set; }
        public string JobFunction { get; set; }
        public double BaseSalary { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double TotalCurrentSalary { get; set; }
        public double mandatoryPercentage { get; set; }
        public double IncreaseInPercentage { get; set; }
        public double NewBaseSalary { get; set; }
        public double NewTotalSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
        public TaskType TaskType { get; set; }
        public string currencyCode { get; set; }
        public double IncreaseInCurrency { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
    }
}
