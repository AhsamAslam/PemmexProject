using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pemmex.Identity.Models
{
    public class TwoFactorViewModel
    {
        [Required]
        public string TwoFactorCode { get; set; }

    }
}
