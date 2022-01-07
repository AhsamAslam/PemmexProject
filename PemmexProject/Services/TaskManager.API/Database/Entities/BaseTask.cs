using PemmexCommonLibs.Domain.Common;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Enumerations;

namespace TaskManager.API.Database.Entities
{
    public class BaseTask:AuditableEntity
    {
        [Key]
        public int TaskId { get; set; }
        public Guid TaskIdentifier { get; set; }
        public string RequestedByIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public TaskStatuses appliedStatus { get; set; }
        public TaskStatuses currentTaskStatus { get; set; }
        public TaskType taskType { get; set; }
        public bool isActive { get; set; }
        public string taskDescription { get; set; }
        public TaskReasons reasons { get; set; }
        public DateTime EffectiveDate { get; set; }
        public virtual ChangeCompensation ChangeCompensation { get; set; }
        public virtual BonusTask ChangeBonus { get; set; }
        public virtual ChangeGrade ChangeGrade { get; set; }
        public virtual ChangeHoliday ChangeHoliday { get; set; }
        public virtual ChangeTitle ChangeTitle { get; set; }
        public virtual ChangeManager ChangeManager { get; set; }
        public virtual ChangeTeam ChangeTeam { get; set; }
    }
}
