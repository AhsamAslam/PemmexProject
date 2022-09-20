using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;

namespace Organization.API.Database.Entities
{
    public class EmployeeBase:AuditableEntity
    {
        public Guid Emp_Guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Title { get; set; }
        public DateTime? EmployeeDob { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string Gender { get; set; }
        public string ManagerIdentifier { get; set; }
        [NotMapped]
        public string ManagerName { get; set; }
        public string OrganizationIdentifier { get; set; }
        public string Grade { get; set; }
        public string StreetAddress { get; set; }
        public string HouseNumber { get; set; }
        public string Muncipality { get; set; }
        public string PostalCode { get; set; }
        public string Province { get; set; }
        public string country { get; set; }
        public string CountryCellNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string FirstLanguage { get; set; }
        public string FirstLanguageSkills { get; set; }
        public string SecondLanguage { get; set; }
        public string SecondLanguageSkills { get; set; }
        public string ThirdLanguage { get; set; }
        public string ThirdLanguageSkills { get; set; }
        public JobFunction JobFunction { get; set; }

        public int Shift { get; set; }
        public bool IsActive { get; set; }

    }
}
