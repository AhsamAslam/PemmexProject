using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Entities
{
    public class Organization
    {
        public int Id { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationIdentifier { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Business> Businesses { get; set; }
    }
}
