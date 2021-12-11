using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Enumerations;

namespace TaskManager.API.Dtos
{
    public class ApprovalSettingDetailsDto
    {
        public int rankNo { get; set; }
        public string EmployeeIdentifier { get; set; }
        public OrganizationApprovalStructure ManagerType { get; set; }
    }
}
