using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Models
{
    public class Employee
    {
        public Guid Emp_Guid { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string BusinessName { get; set; }
        public DateTime EmployeeDob { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Title { get; set; }
        public string ContractualOrganization { get; set; }
        public string Grade { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string OrganizationCountry { get; set; }
        public string JobFunction { get; set; }
        public string OrganizationIdentifier { get; set; }

    }
    public class EmployeeCompensation
    {
        public Guid Emp_Guid { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Title { get; set; }
        public string Grade { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string OrganizationCountry { get; set; }
        public string JobFunction { get; set; }
        public double BaseSalary { get; set; }
        public double mandatoryPercentage { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double CarBenefit { get; set; }
        public double InsuranceBenefit { get; set; }
        public double PhoneBenefit { get; set; }
        public double EmissionBenefit { get; set; }
        public double HomeInternetBenefit { get; set; }
        public double TotalMonthlyPay { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string currencyCode { get; set; }
    }
    public class EmployeeBonus
    {
        public DateTime bonusDateTime { get; set; }
        public double bonusAmount { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Title { get; set; }
        public string Grade { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
    }
}
