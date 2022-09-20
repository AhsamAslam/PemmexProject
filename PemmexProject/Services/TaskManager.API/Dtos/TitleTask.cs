using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Dtos
{
    public class TitleTask
    {
        public string oldTitle { get; set; }
        public string NewTitle { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string EmployeeName { get; set; }
        public string organizationIdentifier { get; set; }
        public string businessIdentifier { get; set; }
        public string costcenterIdentifier { get; set; }

    }
}
