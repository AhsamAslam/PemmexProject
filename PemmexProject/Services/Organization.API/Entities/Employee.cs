using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;

namespace Organization.API.Entities
{
    public class Employee : EmployeeBase
    {
        [Key]
        public int EmployeeId { get; set; }
        [ForeignKey("Business")]
        public int BusinessId { get; set; }
        public Business Businesses { get; set; }
        public int CostCenterId { get; set; }
        public CostCenter CostCenter { get; set; }
        public List<EmployeeBonuses> Bonuses { get; set; }
        public List<EmployeeContacts> employeeContacts { get; set; }
        

    }

}
