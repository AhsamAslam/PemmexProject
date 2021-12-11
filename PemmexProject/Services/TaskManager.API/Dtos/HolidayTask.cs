using Holidays.API.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Dtos
{
    public class HolidayTask
    {
        public int HolidayTaskId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public Guid SubsituteId { get; set; }
        public string SubsituteIdentifier { get; set; }
        public DateTime? HolidayStartDate { get; set; }
        public DateTime? HolidayEndDate { get; set; }
        public HolidayTypes holidayType { get; set; }
        public string FileName { get; set; }
    }
}
