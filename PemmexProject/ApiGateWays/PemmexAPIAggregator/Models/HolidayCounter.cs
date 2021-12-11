using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Models
{
    public class HolidayCounter
    {
        public int PlannedHolidays { get; set; }   //Annual Holidays Available {year} 
        public int EarnedHolidays { get; set; }      //Earned Holidays For Next Period  
        public int UsedHolidays { get; set; }        //Used Holidays
        public int LeftHolidays { get; set; }        //Left Holidays
    }
}
