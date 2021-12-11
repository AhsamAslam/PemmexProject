using Holidays.API.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManager.API.Database.Entities
{
    public class ChangeHoliday
    {
        [Key]
        [ForeignKey("BaseTask")]
        public int HolidayTaskId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public Guid SubsituteId { get; set; }
        public string SubsituteIdentifier { get; set; }
        public DateTime? HolidayStartDate { get; set; }
        public DateTime? HolidayEndDate { get; set; }
        public HolidayTypes holidayType { get; set; }
        public string FileName { get; set; }
        public virtual BaseTask BaseTask { get; set; }
    }
}
