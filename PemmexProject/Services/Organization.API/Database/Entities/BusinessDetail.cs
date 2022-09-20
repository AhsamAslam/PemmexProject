using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using PemmexCommonLibs.Domain.Common;

namespace Organization.API.Database.Entities
{
    public class BusinessDetail : AuditableEntity
    {
        [Key]
        public int BusinessDetailId { get; set; }
        public int EmployeeId { get; set; }
        public int BusinessId { get; set; }
        public Employee Employee { get; set; }
        public Business Businesses { get; set; }
        public string LegalOrgZero { get; set; }
        public string LegalOrgOne { get; set; }
        public string LegalOrgTwo { get; set; }
        public string FunctionalOrgZero { get; set; }
        public string FunctionalOrgOne { get; set; }
        public string FunctionalOrgTwo { get; set; }
        public string FunctionalOrgThree { get; set; }
        public string FunctionalOrgFour { get; set; }
        public string FunctionalOrgFive { get; set; }
        public string FunctionalOrgSix { get; set; }
        public string ContractualOrganization { get; set; }
        public string OrganizationCountry { get; set; }
        public int Shift { get; set; }
        
    }
}
