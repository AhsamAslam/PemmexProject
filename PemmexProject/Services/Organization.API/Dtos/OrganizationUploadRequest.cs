using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PemmexCommonLibs.Application.Mappings;
using PemmexCommonLibs.Domain.Enums;

namespace Organization.API.Dtos
{
    public class OrganizationRequestDto
    {
        public int Id { get; set; }
        ////[ExcelColumn("Name")] //maps the "Name" property to the "Name" column
        public string Name { get; set; }
        ////[ExcelColumn("Number")] //maps the "Name" property to the "Org Number" column
        public string OrgNumber { get; set; }
        ////[ExcelColumn("Parent")] //maps the "Name" property to the "Parent" column

    }
    public class BusinessRequestDto
    {
        public int Id { get; set; }
        ////[ExcelColumn("Name")] //maps the "Name" property to the "Name" column
        public string Name { get; set; }
        ////[ExcelColumn("Number")] //maps the "Name" property to the "Org Number" column
        public string OrgNumber { get; set; }
        ////[ExcelColumn("Parent")] //maps the "Name" property to the "Parent" column
        public string ParentNumber { get; set; }

        public string OrganizationCountry { get; set; }
        public string FileName { get; set; }
        public List<EmployeeUploadRequest> Employees { get; set; }

    }
    public class EmployeeUploadRequest  
    {
        public int Id { get; set; }
        public Guid Emp_Guid { get; set; }
        ////[ExcelColumn("First Name")] //maps the "First Name" property to the "FirstName" column
        public string FirstName { get; set; }
        public string UserName { get; set; }
        //[ExcelColumn("Last Name")] //maps the "Last Name" property to the "LastName" column
        public string LastName { get; set; }

        //[ExcelColumn("Middle Name")] //maps the "Middle Name" property to the "MiddleName" column
        public string MiddleName { get; set; }
        //[ExcelColumn("Title")] //maps the "Title" property to the "Title" column
        public string Title { get; set; }

        //[ExcelColumn("Date of Birth")] //maps the "Employee Dob" property to the "EmployeeDob" column
        public string EmployeeDob { get; set; }
        //[ExcelColumn("Organization Identifier (Employee)")] //maps the "Organization Identifier" property to the "OrganizationIdentifier" column
        public string EmployeeIdentifier { get; set; }
        //[ExcelColumn("Gender")] //maps the "Gender" property to the "Gender" column
        public string Gender { get; set; }
        //[ExcelColumn("Manager's Org Identifier")] //maps the "Manager's Org. Identifier" property to the "ManagerIdentifier" column
        public string ManagerIdentifier { get; set; }
        //[ExcelColumn("Manager/Superior")] //maps the "Manager Name" property to the "ManagerName" column
        //[ExcelColumn("Cost Center")] //maps the "Cost Center" property to the "CostCenter" column
        public string CostCenterIdentifier { get; set; }
        //[ExcelColumn("Cost Center Name")] //maps the "Cost Center Name" property to the "CostCenterName" column
        public string Country { get; set; }
        //[ExcelColumn("Grade")] //maps the "Grade" property to the "Grade" column
        public string Grade { get; set; }
        
        //[ExcelColumn("Street Address")] //maps the "Shift" property to the "Street Address" column
        public string StreetAddress { get; set; }
        //[ExcelColumn("House Number")] //maps the "Shift" property to the "House Number" column
        public string HouseNumber { get; set; }
        //[ExcelColumn("Muncipality")] //maps the "Shift" property to the "Muncipality" column
        public string Muncipality { get; set; }
        //[ExcelColumn("Postal Code")] //maps the "Shift" property to the "Postal Code" column
        public string PostalCode { get; set; }
        //[ExcelColumn("Province")] //maps the "Shift" property to the "Province" column
        public string Province { get; set; }
        //[ExcelColumn("Cell Number")] //maps the "Shift" property to the "Country Cell Number" column
        public string CellNumber { get; set; }
        //[ExcelColumn("Phone Number")] //maps the "Shift" property to the "Phone Number" column
        public string PhoneNumber { get; set; }
        //[ExcelColumn("Email")] //maps the "Shift" property to the "Email" column
        public string Email { get; set; }
        //[ExcelColumn("Nationality")] //maps the "Shift" property to the "Nationality" column
        public string Nationality { get; set; }
        //[ExcelColumn("First Language")] //maps the "Shift" property to the "First Language" column
        public string FirstLanguage { get; set; }
        //[ExcelColumn("First Language Skills")] //maps the "Shift" property to the "First Language Skills" column
        public LanguageSkills? FirstLanguageSkills { get; set; }
        //[ExcelColumn("Second Language")] //maps the "Shift" property to the "SecondLanguage" column
        public string SecondLanguage { get; set; }
        //[ExcelColumn("Second Language Skills")] //maps the "Shift" property to the "SecondLanguageSkills" column
        public LanguageSkills? SecondLanguageSkills { get; set; }
        //[ExcelColumn("Third Language")] //maps the "Shift" property to the "ThirdLanguage" column
        public string ThirdLanguage { get; set; }
        //[ExcelColumn("Third Language Skills")] //maps the "Shift" property to the "ThirdLanguageSkills" column
        public LanguageSkills? ThirdLanguageSkills { get; set; }
        public string CountryCellNumber { get; set; }
        public string Role { get; set; }
        public string JobFunction { get; set; }
        public int Shift { get; set; }
        public string ContractualOrganization { get; set; }
        public string OrganizationIdentifier { get; set; }
        public string LegalOrganizationIdentifier { get; set; }
        public int CostCenterId { get; set; }
        public string FunctionalOrganizationIdentifier { get; set; }
        public List<EmployeeContactUpload> employeeContacts { get; set; }
        public CostCenterUploadRequest CostCenter { get; set; }
        public HolidayUploadRequest holidayUploadRequest { get; set; }
        public TimeTableUploadRequest TimeTableUploadRequest { get; set; }
        public bool IsActive { get; set; }
    }

    public class BusinessDetailRequest
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessIdentifier { get; set; }
        public string ParentBusinessId { get; set; }

        public bool IsActive { get; set; }
        //[ExcelColumn("Organization Number")] //maps the "Organization Number" property to the "OrganizationNumber" column
        ////[ExcelColumn("Legal Org 0")] //maps the "Legal Org Zero" property to the "LegalOrgZero" column
        //public string LegalOrgZero { get; set; }
        ////[ExcelColumn("Legal Org 1")] //maps the "Legal Org One" property to the "LegalOrgOne" column
        //public string LegalOrgOne { get; set; }
        ////[ExcelColumn("Legal Org 2")] //maps the "Legal Org Two" property to the "LegalOrgTwo" column
        //public string LegalOrgTwo { get; set; }
        ////[ExcelColumn("Functional Org 0")] //maps the "Functional Org One" property to the "FunctionalOrgOne" column
        //public string FunctionalOrgZero { get; set; }
        ////[ExcelColumn("Functional Org 1")] //maps the "Functional Org Two" property to the "FunctionalOrgTwo" column
        //public string FunctionalOrgOne { get; set; }
        ////[ExcelColumn("Functional Org 2")] //maps the "Functional Org Two" property to the "FunctionalOrgTwo" column
        //public string FunctionalOrgTwo { get; set; }
        ////[ExcelColumn("Functional Org 3")] //maps the "Functional Org Three" property to the "FunctionalOrgThree" column
        //public string FunctionalOrgThree { get; set; }
        ////[ExcelColumn("Functional Org 4")] //maps the "Functional Org Four" property to the "FunctionalOrgFour" column
        //public string FunctionalOrgFour { get; set; }
        ////[ExcelColumn("Functional Org 5")] //maps the "Functional Org Five" property to the "FunctionalOrgFive" column
        //public string FunctionalOrgFive { get; set; }
        ////[ExcelColumn("Functional Org 6")] //maps the "Functional Org Six" property to the "FunctionalOrgSix" column
        //public string FunctionalOrgSix { get; set; }
        ////[ExcelColumn("Shift")] //maps the "Shift" property to the "Shift" column
        //[ExcelColumn("Organization Identifier (Employee)")] //maps the "Employee Identifier" property to the "EmployeeIdentifier" column
        public List<EmployeeUploadRequest> Employees { get; set; }
        
    }
    public class EmployeeContactUpload
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Relationship { get; set; }
    }
    public class CostCenterUploadRequest
    {
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string ParentCostCenterIdentifier { get; set; }
        public string businessIdentifier { get; set; }
    }
    public class CompensationUploadRequest
    {
        //[ExcelColumn("Organization Identifier (Employee)")] //maps the "Organization Identifier" property to the "OrganizationIdentifier" column
        public string EmployeeIdentifier { get; set; }
        //[ExcelColumn("Base Salary")] //maps the "Base Salary" property to the "BaseSalary" column
        public double BaseSalary { get; set; }
        //[ExcelColumn("Additional Agreed Part")] //maps the "Additional Agreed Part" property to the "AdditionalAgreedPart" column
        public double AdditionalAgreedPart { get; set; }
        //[ExcelColumn("Car Benefit")] //maps the "Car Benefit" property to the "CarBenefit" column
        public double CarBenefit { get; set; }
        //[ExcelColumn("Insurance Benefit")] //maps the "Insurance Benefit" property to the "InsuranceBenefit" column
        public double InsuranceBenefit { get; set; }
        //[ExcelColumn("Phone Benefit")] //maps the "Phone Benefit" property to the "PhoneBenefit" column
        public double PhoneBenefit { get; set; }
        //[ExcelColumn("Emission Benefit")] //maps the "Emission Benefit" property to the "EmissionBenefit" column
        public double EmissionBenefit { get; set; }
        //[ExcelColumn("Home Internet Benefit")] //maps the "Home Internet Benefit" property to the "HomeInternetBenefit" column
        public double HomeInternetBenefit { get; set; }
        //[ExcelColumn("Total Monthly Pay")] //maps the "Total Monthly Pay" property to the "TotalMonthlyPay" column
        public double TotalMonthlyPay { get; set; }
        public DateTime EffectiveDate { get; set; }

    }
    public class HolidayUploadRequest
    {
        public string EmployeeIdentifier { get; set; }
        public int AnnualHolidaysEntitled { get; set; }
        public int AccruedHolidaysPreviousYear { get; set; }
        public int UsedHolidaysCurrentYear { get; set; }
        public int LeftHolidaysCurrentYear { get; set; }
        public int ParentalHolidays { get; set; }
        public int AnnualHolidays { get; set; }
        public int AgreementAnnualHolidays { get; set; }
        public int SickLeave { get; set; }
        public int TimeOffWithoutSalary { get; set; }
        public DateTime? EmployementStartDate { get; set; }

    }
    public class TimeTableUploadRequest
    {
        public string EmployeeIdentifier { get; set; }
        public double FlexibleHours { get; set; }
        public DateTime? StartHours { get; set; }
        public DateTime? EndHours { get; set; }
        public double WeeklyHours { get; set; }
        public string TaskDescription { get; set; }
        public double FlexibleHrsAdvertiseAndAccept { get; set; }
    }
}
