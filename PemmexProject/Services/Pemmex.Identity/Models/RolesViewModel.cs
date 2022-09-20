using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pemmex.Identity.Models
{
    public class RolesViewModel
    {
        [Required]
        public string RoleName { get; set; }
        public string ReturnUrl { get; set; }
    }
}
