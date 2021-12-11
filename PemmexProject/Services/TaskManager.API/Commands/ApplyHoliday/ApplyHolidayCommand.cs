using AutoMapper;
using Holidays.API.Enumerations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.context;
using TaskManager.API.Database.Entities;
using TaskManager.API.Enumerations;

namespace TaskManager.API.Commands.ApplyHoliday
{
    public class ApplyHolidayCommand : IRequest
    {
        public Guid? SubsituteId { get; set; }
        public string SubsituteIdentifier { get; set; }
        public DateTime? HolidayStartDate { get; set; }
        public DateTime? HolidayEndDate { get; set; }
        public HolidayTypes holidayType { get; set; }
        [JsonIgnore]
        public string EmployeeIdentifier { get; set; }
        [JsonIgnore]
        public string ManagerIdentifier { get; set; }
        public string taskDescription { get; set; }
        [JsonIgnore]
        public Guid UserId { get; set; }
        [JsonIgnore]
        public string FileName { get; set; }
        [JsonIgnore]
        public string organizationIdentifier { get; set; }
    }

    public class ApplyHolidayCommandHandeler : IRequestHandler<ApplyHolidayCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        public ApplyHolidayCommandHandeler(IApplicationDbContext context, IMapper mapper,
            IDateTime dateTime)
        {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(ApplyHolidayCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.ManagerIdentifier))
                    throw new Exception("There is no manager to approve the request");


                var approvesetting = await GetApprovalSettingStructure(request.organizationIdentifier);
                if (approvesetting == null)
                {
                    throw new Exception("Approval settings does not contain any approval structure");
                }


                var baseTask = new BaseTask();
                var holiday = _mapper.Map<ApplyHolidayCommand, ChangeHoliday>(request);
                baseTask.isActive = true;
                baseTask.currentTaskStatus = TaskStatuses.Pending;
                baseTask.appliedStatus = TaskStatuses.Initiated;
                baseTask.CreatedBy = request.UserId.ToString();
                baseTask.Created = _dateTime.Now;
                baseTask.taskType = TaskType.Holiday;
                baseTask.ChangeHoliday = holiday;
                baseTask.taskDescription = request.taskDescription;
                baseTask.RequestedByIdentifier = request.EmployeeIdentifier;
                baseTask.TaskIdentifier = Guid.NewGuid();
                baseTask.ManagerIdentifier = request.ManagerIdentifier;
                _context.BaseTasks.Add(baseTask);
                var managerTask = PopulateManagerTask(baseTask);
                if (approvesetting.ManagerType == OrganizationApprovalStructure.Other)
                {
                    managerTask.RequestedByIdentifier = approvesetting.EmployeeIdentifier;
                }

                _context.BaseTasks.Add(managerTask);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch(Exception)
            {
                throw;
            }

        }
        private async Task<OrganizationApprovalSettingDetail> GetApprovalSettingStructure(string organizationIdentifer)
        {
            var setting = await _context.organizationApprovalSettings
                  .Include(d => d.organizationApprovalSettingDetails)
                   .FirstOrDefaultAsync(s => s.OrganizationIdentifier == organizationIdentifer
                   && s.taskType == TaskType.Holiday);

            return setting.organizationApprovalSettingDetails?.FirstOrDefault();
        }
        private BaseTask PopulateManagerTask(BaseTask dto)
        {
            BaseTask task = new BaseTask();

            task.isActive = true;
            task.ManagerIdentifier = null;
            task.RequestedByIdentifier = dto.ManagerIdentifier;
            task.currentTaskStatus = TaskStatuses.Pending;
            task.appliedStatus = TaskStatuses.Pending;
            task.taskType = dto.taskType;
            task.CreatedBy = dto.CreatedBy;
            task.Created = _dateTime.Now;
            task.taskDescription = dto.taskDescription;
            task.TaskIdentifier = dto.TaskIdentifier;
            
            return task;
        }
    }
}