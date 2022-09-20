using Microsoft.AspNetCore.Identity;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;

namespace Pemmex.Identity.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }   
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string OrganizationIdentifier { get; set; }
        public bool IsPasswordReset { get; set; }
        public string PasswordResetCode { get; set; }
        public DateTime PasswordResetCodeTime { get; set; }
        public bool isActive { get; set; }
        public JobFunction JobFunction { get; set; }
        public string Grade { get; set; }
        public string OrganizationCountry { get; set; }
        public ICollection<UserBusinessUnits> UserBusinessUnits { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; } = new List<ApplicationUserRole>();
    }
}
