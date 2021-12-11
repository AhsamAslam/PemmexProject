using Holidays.API.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Dtos
{
    public class TakenHolidayDto
    {
        public int EmployeeHolidayId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string costcenterIdentifier { get; set; }
        public Guid SubsituteId { get; set; }
        public string SubsituteIdentifier { get; set; }
        public DateTime? HolidayStartDate { get; set; }
        public DateTime? HolidayEndDate { get; set; }
        public string HolidayStatus { get; set; }
        public HolidayTypes holidayType { get; set; }
        public string Description { get; set; }
        public int TotalDays { get; set; }
    }
}