using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskManager.API.Database.Entities;
using TaskManager.API.Enumerations;

namespace TaskManager.API.Dtos
{
    public class TaskDto
    {
        public int TaskId { get; set; }
        public Guid TaskIdentifier { get; set; }
        public string RequestedByIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public TaskStatuses appliedStatus { get; set; }
        public TaskStatuses currentTaskStatus { get; set; }
        public TaskType taskType { get; set; }
        public string taskDescription { get; set; }
        public bool isActive { get; set; }
        public TaskReasons reasons { get; set; }
        public DateTime EffectiveDate { get; set; }
        public CompensationTask compensationTask { get; set; }
        public HolidayTask holidayTask { get; set; }
        public TitleTask titleTask { get; set; }
        public ManagerTask managerTask { get; set; }
        public GradeTask GradeTask { get; set; }
        public TeamTask TeamTask { get; set; }
        public BonusTask BonusTask { get; set; }
    }
}
