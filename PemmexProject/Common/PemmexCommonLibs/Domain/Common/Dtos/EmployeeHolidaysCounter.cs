using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Common.Dtos
{
    public class EmployeeHolidaysCounter
    {
        public int PlannedHolidays { get; set; }   //Annual Holidays Available {year} 
        public int EarnedHolidays { get; set; }      //Earned Holidays For Next Period  
        public int UsedHolidays { get; set; }        //Used Holidays
        public int LeftHolidays { get; set; }        //Left Holidays
        public Guid Emp_Guid { get; set; }
        public string EmployeeIdentifier { get; set; }
    }
}
