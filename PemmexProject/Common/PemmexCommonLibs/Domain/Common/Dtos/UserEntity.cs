using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PemmexCommonLibs.Domain.Common.Dtos
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string OrganizationIdentifier { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Role { get; set; }
        public JobFunction JobFunction { get; set; }
        public string Grade { get; set; }
        public bool IsPasswordReset { get; set; }
        public bool IsTwoStepAuthEnabled { get; set; }
        public string OrganizationCountry { get; set; }

    }
    public class RolesEntity
    {
        public string Id { get; set; }
        public string label { get; set; }
        public string value { get; set; }
        public string roleName { get; set; }
        public string color { get; set; }
        public string OrganizationIdentifier { get; set; }
        public List<ScreenEntity> screenEntities { get; set; }
    }
    public class ScreenEntity
    {
        public string screenName { get; set; }
    }
}
