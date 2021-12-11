using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Dtos
{
    public class BusinessDetailVM
    {
        public int EmployeeId { get; set; }
        public int BusinessId { get; set; }
        public EmployeeResponse Employee { get; private set; } = new EmployeeResponse();
        public string ContractualOrganization { get; set; }
        public string LegalOrgZero { get; set; }
        public string LegalOrgOne { get; set; }
        public string LegalOrgTwo { get; set; }
        public string FunctionalOrgOne { get; set; }
        public string FunctionalOrgTwo { get; set; }
        public string FunctionalOrgThree { get; set; }
        public string FunctionalOrgFour { get; set; }
        public string FunctionalOrgFive { get; set; }
        public string FunctionalOrgSix { get; set; }
        public int Shift { get; set; }
    }
}
