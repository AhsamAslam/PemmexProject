using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pemmex.Identity.Models
{
    public class UserBusinessUnits
    {
        public int Id { get; set; }
        public string BUnitIdentifier { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
