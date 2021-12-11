using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API.Database.Entities
{
    public class Screens:AuditableEntity
    {
        [Key]
        public int screenId { get; set; }
        public string screenName { get; set; }
        [ForeignKey("Roles")]
        public int roleId { get; set; }
        public Roles Roles { get; set; }
       
    }
}
