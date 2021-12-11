using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Dtos
{
    public class CompanyToEmployeeHolidayDto
    {
        public int CompanyToEmployeeHolidayId { get; set; }
        public Guid EmployeeId { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string costcenterIdentifier { get; set; }
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
        public Guid HolidaySettingsIdentitfier { get; set; }


    }
}
