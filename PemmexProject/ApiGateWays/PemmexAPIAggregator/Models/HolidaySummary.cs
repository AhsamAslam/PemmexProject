using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Models
{
    public class HolidaySummary
    {
        public string EmployeeIdentifier { get; set; }
        public Guid Emp_Guid { get; set; }
        public string BusinessIdentifier { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ContractualOrganization { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string CostCenterName { get; set; }
        public string OrganizationCountry { get; set; }
        public int PlannedHolidays { get; set; }   //Annual Holidays Available {year} 
        public int EarnedHolidays { get; set; }      //Earned Holidays For Next Period  
        public int UsedHolidays { get; set; }        //Used Holidays
        public int LeftHolidays { get; set; }        //Left Holidays
    }
}
