using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Common.Dtos
{
    public class CompanyToEmployeeHolidayEntity
    {
        public int EmployeeHolidayId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string costcenterIdentifier { get; set; }
        public int AnnualHolidaysEntitled { get; set; }
        public int AccruedHolidaysPreviousYear { get; set; }
        public int EarnHolidaysCurrentYear { get; set; }
        public int UsedHolidaysCurrentYear { get; set; }
        public int LeftHolidaysCurrentYear { get; set; }
        public int ParentalHolidays { get; set; }
        public int AnnualHolidays { get; set; }
        public int AgreementAnnualHolidays { get; set; }
        public int SickLeave { get; set; }
        public int TimeOffWithoutSalary { get; set; }
        public DateTime? EmployementStartDate { get; set; }
        public Guid HolidaySettingsIdentitfier { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
    }
    public class HolidaySettingsEntity
    {
        public int HolidaySettingsId { get; set; }
        public string OrganizationIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public DateTime HolidayCalendarYear { get; set; }
        public int MaximumLimitHolidayToNextYear { get; set; }
        public Guid HolidaySettingsIdentitfier { get; set; }

    }
    public class EmployeeHolidayEntity
    {
        public int EmployeeHolidayId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string costcenterIdentifier { get; set; }
        public string SubsituteIdentifier { get; set; }
        public DateTime? HolidayStartDate { get; set; }
        public DateTime? HolidayEndDate { get; set; }
        public HolidayStatus HolidayStatus { get; set; }
        public HolidayTypes holidayType { get; set; }
        public string Description { get; set; }
    }
}
