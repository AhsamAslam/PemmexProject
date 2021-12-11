using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Dtos
{
    public class TokenUser
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
        public bool IsPasswordReset { get; set; }
        public bool IsTwoStepAuthEnabled { get; set; }
        public string PasswordResetCode { get; set; }
        public DateTime PasswordResetCodeTime { get; set; }
        public bool isActive { get; set; }
        public string JobFunction { get; set; }
        public string Grade { get; set; }
        public string OrganizationCountry { get; set; }
        public TokenObject tokenObject { get; private set; } = new TokenObject();

    }
    public class TokenObject
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string token_type { get; set; }
        public string scope { get; set; }
    }
}
