using PemmexCommonLibs.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Entities
{
    public class EmployeeContacts: AuditableEntity
    {
        [Key]
        public int EmployeeContactId { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Relationship { get; set; }
        public Employee Employee { get; set; }
    }
}
