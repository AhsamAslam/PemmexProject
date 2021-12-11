using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Database.Entities
{
    public class HolidayCalendar
    {
        [Key]
        public int HolidayCalendarId { get; set; }
        public DateTime Date { get; set; }
        public string CountryCode { get; set; }
        public string LocalName { get; set; }
        public string Name { get; set; }
        public bool Global { get; set; }
        public bool Fixed { get; set; }
        public string Type { get; set; }
    }
}
