using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PemmexCommonLibs.Domain.Enums;

namespace Organization.API.Dtos
{
    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public Guid Emp_Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string MiddleName { get; set; }
        public DateTime EmployeeDob { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string Gender { get; set; }
        public string ManagerIdentifier { get; set; }
        public string Country { get; set; }
        public string Grade { get; set; }
        public string StreetAddress { get; set; }
        public string HouseNumber { get; set; }
        public string Muncipality { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public string CountryCellNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string FirstLanguage { get; set; }
        public LanguageSkills FirstLanguageSkills { get; set; }
        public string SecondLanguage { get; set; }
        public LanguageSkills SecondLanguageSkills { get; set; }
        public string ThirdLanguage { get; set; }
        public LanguageSkills ThirdLanguageSkills { get; set; }
        public string OrganizationCountry { get; set; }
        public int Shift { get; set; }
        public string OrganizationIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string JobFunction { get; set; }
        public List<CompensationResponse> Compensation { get;  set; }
        public CostCenterResponse CostCenter { get; set; }

        public EmployeeHolidaysResponse Holidays { get; set; }
        public List<EmployeeContactResponse> Contacts { get; set; }
        public List<EmployeeResponse> children { get; set; }
        public int? TotalEmployee
        {
            get { return 1 + children?.Sum(x => x.TotalEmployee ?? 0); }
        }
    }
    public class CostCenterResponse
    {
        public int CostCenterId { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string ParentCostCenterIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public List<CostCenterResponse> children { get; set; }
    }
    public class CompensationResponse
    {
        public double BaseSalary { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double CarBenefit { get; set; }
        public double InsuranceBenefit { get; set; }
        public double PhoneBenefit { get; set; }
        public double EmissionBenefit { get; set; }
        public double HomeInternetBenefit { get; set; }
        public double TotalMonthlyPay { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
    public class EmployeeHolidaysResponse
    {
        public int ParentalHolidays { get; set; }
        public int AnnualHolidays { get; set; }
        public int AgreementAnnualHolidays { get; set; }
        public int SickLeave { get; set; }
        public string HolidayStartDate { get; set; }
        public string HolidayEndDate { get; set; }
        public int TimeOffWithoutSalary { get; set; }
    }
    public class EmployeeContactResponse
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Relationship { get; set; }
    }
}
