using PemmexCommonLibs.Domain.Enums;
using System;
namespace PemmexAPIAggregator.Models.Holidays
{
    public class EmployeeHolidayDto
    {
        public int EmployeeHolidayId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string costcenterIdentifier { get; set; }
        public string SubsituteIdentifier { get; set; }
        public string SubsituteName { get; set; }
        public DateTime? HolidayStartDate { get; set; }
        public DateTime? HolidayEndDate { get; set; }
        public string HolidayStatus { get; set; }
        public HolidayTypes holidayType { get; set; }
        public string Description { get; set; }
        public int TotalDays { get; set; }
    }
}
