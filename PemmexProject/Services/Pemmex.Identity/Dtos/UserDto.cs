using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pemmex.Identity.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string OrganizationIdentifier { get; set; }
        public string UserName { get; set; }
        public bool isActive { get; set; }
        public string Grade { get; set; }
        public string OrganizationCountry { get; set; }
        public bool IsPasswordReset { get; set; }
        public List<string> Role { get; set; } = new List<string>();
        public List<string> ManagerHeirarchy { get; set; } = new List<string>();
        public JobFunction JobFunction { get; set; }
    }
}
