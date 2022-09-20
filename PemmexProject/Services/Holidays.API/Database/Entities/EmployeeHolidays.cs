using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Holidays.API.Database.Entities
{
    public class EmployeeHolidays:AuditableEntity
    {
        [Key]
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
