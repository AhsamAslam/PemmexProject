using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Models
{
    public class Business
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }

        public string BusinessIdentifier { get; set; }
        public string ParentBusinessId { get; set; }
    }
}
