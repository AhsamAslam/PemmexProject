using Holidays.API.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Holidays.API.Dtos
{
    public class HolidayEmployeeCounter
    {

        public string UserIdentifier { get; set; }
        public int AccruedHolidayForCurrentYear { get; set; }   //Annual Holidays Available {year} 
        public int AccruedHolidayForNextYear { get; set; }      //Earned Holidays For Next Period  
        public int UsedHolidaysCurrentYear { get; set; }        //Used Holidays
        public int LeftHolidaysCurrentYear { get; set; }        //Left Holidays
    }
}
