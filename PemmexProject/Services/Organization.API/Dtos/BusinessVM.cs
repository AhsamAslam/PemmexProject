using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Dtos
{
    public class BusinessVM
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }

        public string BusinessIdentifier { get; set; }
        public string ParentBusinessId { get; set; }
        public string OrganizationCountry { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }

    }
}
