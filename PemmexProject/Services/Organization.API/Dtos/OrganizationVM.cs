using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Dtos
{
    public class OrganizationVM
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationIdentifier { get; set; }
        public bool IsActive { get; set; }
        public ICollection<BusinessVM> OrganizationDetails { get; private set; } = new List<BusinessVM>();
    }
}
