using AutoMapper;
using EventBus.Messages.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.API.Database.context;
using TaskManager.API.Database.Entities;
using TaskManager.API.Dtos;
using TaskManager.API.Enumerations;
using TaskManager.API.NotificationHub;

namespace TaskManager.API.Commands.UpdateTask
{
    public class UpdateTask : IRequest
    {
        public Guid taskId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string organizationIdentifier { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string requestIdentifier { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public Guid userId { get; set; }
        public TaskStatuses taskStatuses { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string managerIdentifier { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string token { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string costcenterIdentifier { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string businessIdentifier { get; set; }
        public TaskReasons reasons { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string taskDescription { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string EmployeeName { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string ManagerName { get; set; }
        public UpdateCompensationTask compensationTask { get; set; }
        public UpdateHolidayTask holidayTask { get; set; }
        public UpdateTitleTask titleTask { get; set; }
        public UpdateGradeTask gradeTask { get; set; }
        public UpdateBonusTask bonusTask { get; set; }
        public UpdateBudgetPromotionTask budgetPromotionTask { get; set; }

    }

    public class UpdateTaskCommandHandeler : IRequestHandler<UpdateTask>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDateTime _dateTime;
        private readonly IConfiguration _configuration;
        private readonly ITopicPublisher _messagePublisher;
        private readonly IUserConnectionManager _userConnectionManager;
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationUserHub> _notificationUserHubContext;
        public UpdateTaskCommandHandeler(IApplicationDbContext context, IMapper mapper,
            IDateTime dateTime, IConfiguration configuration, 
            ITopicPublisher messagePublisher,
             IUserConnectionManager userConnectionManager,
            INotificationRepository notificationRepository,
            IHubContext<NotificationUserHub> notificationUserHubContext)
        {
            _context = context;
            _mapper = mapper;
            _dateTime = dateTime;
            _configuration = configuration;
            _messagePublisher = messagePublisher;
            _notificationRepository = notificationRepository;
            _userConnectionManager = userConnectionManager;
            _notificationUserHubContext = notificationUserHubContext;
            
        }

        public async Task<Unit> Handle(UpdateTask request, CancellationToken cancellationToken)
        {
            try
            {
                var tasks = await _context.BaseTasks
                            .Where(b => b.TaskIdentifier == request.taskId)
                            .ToListAsync();

                if (request.taskStatuses == TaskStatuses.Approved)
                {
                    if (tasks.Count < 2)
                    {
                        throw new Exception("There is no record to get approved");
                    }

                    var t = tasks.FirstOrDefault(t => t.RequestedByIdentifier == request.requestIdentifier);
                    t.appliedStatus = request.taskStatuses;
                    t.ManagerIdentifier = request.managerIdentifier;
                    t.ManagerName = request.ManagerName;
                    t.businessIdentifier = request.businessIdentifier;
                    t.costcenterIdentifier = request.costcenterIdentifier;
                    t.organizationIdentifier = request.organizationIdentifier;
                    t.currentTaskStatus = TaskStatuses.Pending;
                    t.taskDescription = request.taskDescription;
                    var approvesettingArray = await GetApprovalSettingStructure(request.organizationIdentifier,t.taskType);
                    if (approvesettingArray.Count <= 0)
                    {
                        throw new Exception("Approval settings does not contain any approval structure");
                    }
                    var approveSetting = approvesettingArray.FirstOrDefault(s => s.rankNo == tasks.Count() - 2);
                    if(approveSetting.rankNo == (approvesettingArray.Count - 1))
                    {

                        t.currentTaskStatus = TaskStatuses.Approved;
                        t = await PopulateDetailTask(t, request);
                        _context.BaseTasks.Update(t);
                        tasks.ForEach(t =>
                                t.currentTaskStatus = TaskStatuses.Approved);
                        _context.BaseTasks.UpdateRange(tasks);
                        await _context.SaveChangesAsync(cancellationToken);
                        if(t.taskType == TaskType.Holiday)
                        {
                            await PublishHolidayData(t, request);
                        }
                        else if (t.taskType == TaskType.BudgetPromotion)
                        {
                            await PublishBudgetPromotionData(t, request);
                        }
                        else if (t.taskType == TaskType.Compensation)
                        {
                            await PublishCompensationData(t, request);
                        }
                        else
                        {
                            await PublishOrganizationData(t,request);
                        }
                        Notifications n = new Notifications()
                        {
                            isRead = false,
                            tasks = t.taskType,
                            description = "The workflows are all completed and updated",
                            title = "Workflow Updated",
                            EmployeeId = request.requestIdentifier.ToString()
                        };
                        await SendNotification(cancellationToken, n);
                    }
                    else
                    {
                        var managerTask = PopulateManagerTask(t);
                        var s = approvesettingArray.FirstOrDefault(s => s.rankNo == approveSetting.rankNo + 1);
                        if (s.ManagerType == OrganizationApprovalStructure.Other)
                        {
                            managerTask.RequestedByIdentifier = s.EmployeeIdentifier;
                        }
                        t = await PopulateDetailTask(t, request);
                        _context.BaseTasks.Add(managerTask);
                        _context.BaseTasks.Update(t);
                        await _context.SaveChangesAsync(cancellationToken);
                        Notifications n = new Notifications()
                        {
                            isRead = false,
                            tasks = t.taskType,
                            description = "There is a Request to approve Please see the details",
                            title = "Workflow Updated",
                            EmployeeId = t.RequestedByIdentifier
                        };
                        await SendNotification(cancellationToken, n);
                    }
                }
                else
                {
                    var t = tasks.FirstOrDefault(t => t.RequestedByIdentifier == request.requestIdentifier);
                    t.appliedStatus = TaskStatuses.Declined;
                    t.currentTaskStatus = TaskStatuses.Declined;
                    t.businessIdentifier = request.businessIdentifier;
                    t.costcenterIdentifier = request.costcenterIdentifier;
                    t.organizationIdentifier = request.organizationIdentifier;
                    t.ManagerIdentifier = request.managerIdentifier;
                    tasks.ForEach(t => 
                    t.currentTaskStatus = TaskStatuses.Declined);
                    _context.BaseTasks.UpdateRange(tasks);
                    await _context.SaveChangesAsync(cancellationToken);
                    Notifications n = new Notifications()
                    {
                        isRead = false,
                        tasks = t.taskType,
                        description = "There is a Request to approve Please see the details",
                        title = "Workflow Updated",
                        EmployeeId = t.RequestedByIdentifier
                    };
                    await SendNotification(cancellationToken, n);
                }
                return Unit.Value;
            }
            catch(Exception)
            {
                throw;
            }
        }
        private BaseTask PopulateManagerTask(BaseTask dto)
        {
            BaseTask task = new BaseTask();

            task.isActive = true;
            task.ManagerIdentifier = null;
            task.RequestedByIdentifier = dto.ManagerIdentifier;
            task.RequesterName = dto.ManagerName;
            task.currentTaskStatus = TaskStatuses.Pending;
            task.appliedStatus = TaskStatuses.Pending;
            task.taskType = dto.taskType;
            task.CreatedBy = dto.CreatedBy;
            task.Created = _dateTime.Now;
            task.taskDescription = dto.taskDescription;
            task.TaskIdentifier = dto.TaskIdentifier;
            task.reasons = dto.reasons;
            task.EffectiveDate = dto.EffectiveDate;

            return task;
        }
        private async Task<List<OrganizationApprovalSettingDetail>> GetApprovalSettingStructure(string organizationIdentifer,TaskType type)
        {
            var setting = await _context.organizationApprovalSettings
                   .Include(d => d.organizationApprovalSettingDetails)
                   .FirstOrDefaultAsync(s => s.OrganizationIdentifier == organizationIdentifer
                   && s.taskType == type);

            return setting.organizationApprovalSettingDetails.ToList();
        }
        
        private async Task PublishHolidayData(BaseTask task, UpdateTask request)
        {
            try
            {
                dynamic json = new ExpandoObject();
                EmployeeHolidayEntity entity = new EmployeeHolidayEntity();
                var taskData = await _context.BaseTasks
                        .Include(h => h.ChangeHoliday)
                        .Where(b => b.TaskIdentifier == task.TaskIdentifier).FirstOrDefaultAsync();
                if (taskData != null)
                {
                    if (task.taskType == TaskType.Holiday && task.ChangeHoliday != null)
                    {
                        entity.HolidayEndDate = task.ChangeHoliday.HolidayEndDate;
                        entity.Description = task.ChangeHoliday.Description;
                        entity.holidayType = taskData.ChangeHoliday.holidayType;
                        entity.HolidayStartDate = task.ChangeHoliday.HolidayStartDate;
                        entity.organizationIdentifier = taskData.ChangeHoliday.organizationIdentifier;
                        entity.businessIdentifier = taskData.ChangeHoliday.businessIdentifier;
                        entity.SubsituteIdentifier = taskData.ChangeHoliday.SubsituteIdentifier;
                        entity.EmployeeIdentifier = taskData.ChangeHoliday.EmployeeIdentifier;
                        entity.costcenterIdentifier = taskData.ChangeHoliday.costcenterIdentifier;
                        entity.HolidayStatus = HolidayStatus.Planned;
                        
                    }
                }
                await _messagePublisher.Publish<EmployeeHolidayEntity>(entity);
            }
            catch(Exception)
            {
                throw;
            }
            
        }

        private async Task PublishBudgetPromotionData(BaseTask task, UpdateTask request)
        {
            dynamic json = new ExpandoObject();
            BudgetPromotionUpdateEntity entity = new BudgetPromotionUpdateEntity();
            var taskData = await _context.BaseTasks
                    .Include(h => h.ChangeBudgetPromotion)
                    .ThenInclude(d => d.changeBudgetPromotionDetails)
                    .Where(b => b.TaskIdentifier == task.TaskIdentifier).FirstOrDefaultAsync();
            if(taskData != null)
            {
                if (taskData.taskType == TaskType.BudgetPromotion && taskData.ChangeBudgetPromotion != null)
                {
                    Parallel.ForEach(taskData.ChangeBudgetPromotion.changeBudgetPromotionDetails, async detail =>
                    {
                        entity.EmployeeIdentifier = detail.EmployeeIdentifier;
                        entity.FirstName = detail.FirstName;
                        entity.LastName = detail.LastName;
                        entity.IncreaseInPercentage = detail.IncreaseInPercentage;
                        entity.NewGrade = detail.NewGrade;
                        entity.NewTitle = detail.NewTitle;
                        entity.AdditionalAgreedPart = detail.AdditionalAgreedPart;
                        entity.BaseSalary = detail.BaseSalary;
                        entity.CurrentGrade = detail.CurrentGrade;
                        entity.CurrentTitle = detail.CurrentTitle;
                        entity.EffectiveDate = request.EffectiveDate;
                        entity.Emp_Guid = detail.Emp_Guid;
                        entity.CostCenterIdentifier = detail.CostCenterIdentifier;
                        entity.CostCenterName = detail.CostCenterName;
                        entity.JobFunction = detail.JobFunction;
                        entity.ManagerIdentifier = detail.ManagerIdentifier;
                        entity.ManagerName = detail.ManagerName;
                        entity.mandatoryPercentage = detail.mandatoryPercentage;
                        entity.NewBaseSalary = detail.NewBaseSalary;
                        entity.OrganizationCountry = detail.OrganizationCountry;
                        entity.TotalCurrentSalary = detail.TotalCurrentSalary;
                        entity.NewTotalSalary = detail.NewTotalSalary;
                        entity.currencyCode = detail.currencyCode;
                        entity.IncreaseInCurrency = detail.IncreaseInCurrency;
                        entity.businessIdentifier = detail.businessIdentifier;
                        entity.organizationIdentifier = detail.organizationIdentifier;
                        await _messagePublisher.Publish<BudgetPromotionUpdateEntity>(entity);
                    });       
                }
            }
        }
        private async Task PublishCompensationData(BaseTask task, UpdateTask request)
        {
            dynamic json = new ExpandoObject();
            CompensationEntity entity = new CompensationEntity();
            var taskData = await _context.BaseTasks
                    .Include(h => h.ChangeCompensation)
                    .Where(b => b.TaskIdentifier == task.TaskIdentifier).FirstOrDefaultAsync();
            if (taskData != null)
            {
                if (task.taskType == TaskType.Compensation && task.ChangeCompensation != null)
                {
                    entity.AdditionalAgreedPart = task.ChangeCompensation.NewAdditionalAgreedPart;
                    entity.BaseSalary = task.ChangeCompensation.AppliedSalary;
                    entity.TotalMonthlyPay = entity.AdditionalAgreedPart + entity.BaseSalary;
                    entity.EmployeeIdentifier = taskData.ChangeCompensation.EmployeeIdentifier;
                    entity.CarBenefit = taskData.ChangeCompensation.CarBenefit;
                    entity.InsuranceBenefit = taskData.ChangeCompensation.InsuranceBenefit;
                    entity.PhoneBenefit = taskData.ChangeCompensation.PhoneBenefit;
                    entity.EmissionBenefit = taskData.ChangeCompensation.EmissionBenefit;
                    entity.HomeInternetBenefit = taskData.ChangeCompensation.HomeInternetBenefit;
                    entity.EffectiveDate = taskData.EffectiveDate;
                    entity.businessIdentifier = taskData.businessIdentifier;
                    entity.organizationIdentifier = taskData.organizationIdentifier;
                    entity.currencyCode = taskData.ChangeCompensation.currencyCode;
                }
            }
            await _messagePublisher.Publish<CompensationEntity>(entity);
        }
        private async Task PublishOrganizationData(BaseTask task,UpdateTask request)
        {
            dynamic json = new ExpandoObject();
            OrganizationUpdateEntity entity = new OrganizationUpdateEntity();
            var taskData = await _context.BaseTasks
                    .Include(h => h.ChangeGrade)
                    .Include(h => h.ChangeManager)
                    .Include(h => h.ChangeTitle)
                    .Include(h => h.ChangeTeam)
                    .Include(h => h.ChangeBonus)
                    .Where(b => b.TaskIdentifier == task.TaskIdentifier).FirstOrDefaultAsync();
            if (taskData != null)
            {
                if (task.taskType == TaskType.Organization)
                {
                    if(task.ChangeGrade != null)
                    {
                        entity.Grade = task.ChangeGrade.newGrade;
                        entity.EmployeeIdentifier = taskData.ChangeGrade.EmployeeIdentifier;
                    }
                    if(task.ChangeManager != null)
                    {
                        entity.ManagerIdentifier = taskData.ChangeManager.newManagerIdentifier;
                        entity.CostCenterIdentifier = taskData.ChangeManager.newCostCenterIdentifier;
                        entity.businessIdentifier = taskData.ChangeManager.newbusinessIdentifier;
                        entity.EmployeeIdentifier = taskData.ChangeManager.EmployeeIdentifier;
                    }

                }
                else if (task.taskType == TaskType.Title && task.ChangeTitle != null)
                {
                    entity.Title = task.ChangeTitle.NewTitle;
                    entity.EmployeeIdentifier = taskData.ChangeTitle.EmployeeIdentifier;
                }
                else if (task.taskType == TaskType.Team && task.ChangeTeam != null)
                {
                    entity.ManagerIdentifier = taskData.ChangeTeam.managerIdentifier;
                    entity.CostCenterIdentifier = taskData.ChangeTeam.newCostCenterIdentifier;
                }
                else if (task.taskType == TaskType.Bonus && task.ChangeBonus != null)
                {
                    entity.EmployeeIdentifier = task.ChangeBonus.EmployeeIdentifier;
                    entity.one_time_bonus = task.ChangeBonus.one_time_bonus;
                    entity.EffectiveDate = taskData.EffectiveDate;
                    entity.businessIdentifier = taskData.ChangeBonus.businessIdentifier;
                    entity.organizationIdentifier = taskData.ChangeBonus.organizationIdentifier;

                }
                entity.TaskType = taskData.taskType;
            }
            await _messagePublisher.Publish<OrganizationUpdateEntity>(entity);
        }
        private async Task<object> GetTaskTypeObject(BaseTask task,Guid? UserId)
        {
            dynamic json = new ExpandoObject();
            var taskData = await _context.BaseTasks
                    .Include(h => h.ChangeHoliday)
                    .Where(b => b.TaskIdentifier == task.TaskIdentifier).FirstOrDefaultAsync();
            if(taskData != null)
            {
                json.EmployeeId = UserId;
                if (task.taskType == TaskType.Holiday && taskData.ChangeHoliday != null)
                {
                    json.EmployeeIdentifier = taskData.RequestedByIdentifier;
                    json.SubsituteId = taskData.ChangeHoliday.SubsituteId;
                    json.SubsituteIdentifier = taskData.ChangeHoliday.SubsituteIdentifier;
                    json.holidayType = taskData.ChangeHoliday.holidayType;
                    json.HolidayEndDate = taskData.ChangeHoliday.HolidayEndDate;
                    json.HolidayStartDate = taskData.ChangeHoliday.HolidayStartDate;
                    json.HolidayStatus = TaskStatuses.Approved;
                    json.Description = task.taskDescription;
                    json.HolidayStatus = 0;
                    json.businessIdentifier = taskData.ChangeHoliday.businessIdentifier;
                    json.organizationIdentifier = taskData.ChangeHoliday.organizationIdentifier;
                    json.costcenterIdentifier = taskData.costcenterIdentifier;
                }
            }
            
            return json;
        }
        private async Task<BaseTask> PopulateDetailTask(BaseTask task, UpdateTask request)
        {
            var taskData = await _context.BaseTasks
                   .Include(h => h.ChangeHoliday)
                   .Include(h => h.ChangeGrade)
                   .Include(h => h.ChangeManager)
                   .Include(h => h.ChangeTeam)
                   .Include(h => h.ChangeBonus)
                   .Include(h => h.ChangeTitle)
                   .Include(h => h.ChangeCompensation)
                   .Include(h => h.ChangeEmployeeSoftTargets)
                   .Include(h => h.ChangeEmployeeHardTargets)
                   .Include(h => h.ChangeBudgetPromotion)
                   .ThenInclude(h => h.changeBudgetPromotionDetails)
                   .Where(b => b.TaskIdentifier == request.taskId).FirstOrDefaultAsync();

            if (task.taskType == TaskType.Holiday && request.holidayTask != null)
            {
                task.ChangeHoliday = new ChangeHoliday()
                {
                    costcenterIdentifier = taskData.ChangeHoliday.costcenterIdentifier,
                    businessIdentifier = taskData.ChangeHoliday.businessIdentifier,
                    Description = request.taskDescription,
                    EmployeeIdentifier = taskData.ChangeHoliday.EmployeeIdentifier,
                    EmployeeName = taskData.ChangeHoliday.EmployeeName,
                    FileName = taskData.ChangeHoliday.FileName,
                    HolidayEndDate = request.holidayTask.HolidayEndDate,
                    HolidayStartDate = request.holidayTask.HolidayStartDate,
                    organizationIdentifier = taskData.ChangeHoliday.organizationIdentifier,
                    SubsituteIdentifier = taskData.ChangeHoliday.SubsituteIdentifier,
                    holidayType = taskData.ChangeHoliday.holidayType,
                    SubsituteId = taskData.ChangeHoliday.SubsituteId,   
                    SubsituteName = taskData.ChangeHoliday.SubsituteName
                };
            }
            else if (task.taskType == TaskType.Compensation && request.compensationTask != null)
            {
                task.ChangeCompensation = new ChangeCompensation()
                {
                    organizationIdentifier = taskData.ChangeCompensation.organizationIdentifier,
                    AdditionalAgreedPart = taskData.ChangeCompensation.AdditionalAgreedPart,
                    AppliedSalary = request.compensationTask.AppliedSalary,
                    BaseSalary = taskData.ChangeCompensation.BaseSalary,
                    businessIdentifier = taskData.ChangeCompensation.businessIdentifier,    
                    CarBenefit = taskData.ChangeCompensation.CarBenefit,    
                    costcenterIdentifier = taskData.ChangeCompensation.costcenterIdentifier,    
                    currencyCode = taskData.ChangeCompensation.currencyCode,
                    EmissionBenefit = taskData.ChangeCompensation.EmissionBenefit,
                    EmployeeIdentifier = taskData.ChangeCompensation.EmployeeIdentifier,    
                    EmployeeName = taskData.ChangeCompensation.EmployeeName,
                    HomeInternetBenefit = taskData.ChangeCompensation.HomeInternetBenefit,
                    InsuranceBenefit = taskData.ChangeCompensation.InsuranceBenefit,
                    NewTotalMonthlyPay = request.compensationTask.NewTotalMonthlyPay,
                    PhoneBenefit = taskData.ChangeCompensation.PhoneBenefit,
                    TotalMonthlyPay = taskData.ChangeCompensation.TotalMonthlyPay,
                    NewAdditionalAgreedPart = request.compensationTask.NewAdditionalAgreedPart
                };
            }
            else if (task.taskType == TaskType.Organization)
            {
                if(request.gradeTask != null)
                {
                    task.ChangeGrade = new ChangeGrade()
                    {
                        businessIdentifier = taskData.ChangeGrade.businessIdentifier,
                        costcenterIdentifier = taskData.ChangeGrade.costcenterIdentifier,
                        EmployeeIdentifier = taskData.ChangeGrade.EmployeeIdentifier,
                        EmployeeName = taskData.ChangeGrade.EmployeeName,
                        oldGrade = taskData.ChangeGrade.oldGrade,
                        organizationIdentifier = taskData.ChangeGrade.organizationIdentifier,
                        newGrade = request.gradeTask.newGrade
                    };
                }
                if(taskData.ChangeManager != null)
                {
                    task.ChangeManager = new ChangeManager()
                    {
                        organizationIdentifier = taskData.ChangeManager.organizationIdentifier,
                        EmployeeIdentifier = taskData.ChangeManager.EmployeeIdentifier,
                        EmployeeName = taskData.ChangeManager.EmployeeName,
                        newbusinessIdentifier = taskData.ChangeManager.newbusinessIdentifier,  
                        newbusinessName = taskData.ChangeManager.newbusinessName,   
                        newCostCenterIdentifier = taskData.ChangeManager.newCostCenterIdentifier,
                        newCostCenterName = taskData.ChangeManager.newCostCenterName,
                        newManagerIdentifier = taskData.ChangeManager.newManagerIdentifier,
                        newManagerName = taskData.ChangeManager.newManagerName,
                        oldbusinessIdentifier = taskData.ChangeManager.oldbusinessIdentifier,
                        oldCostCenterIdentifier = taskData.ChangeManager.oldCostCenterIdentifier,
                        oldManagerIdentifier = taskData.ChangeManager.oldManagerIdentifier,
                        oldManagerName = taskData.ChangeManager.oldManagerName,
                        oldCostCenterName = taskData.ChangeManager.oldCostCenterName,

                    };
                }
                if (request.compensationTask != null)
                {
                    task.ChangeCompensation = new ChangeCompensation()
                    {
                        organizationIdentifier = taskData.ChangeCompensation.organizationIdentifier,
                        AdditionalAgreedPart = taskData.ChangeCompensation.AdditionalAgreedPart,
                        AppliedSalary = request.compensationTask.AppliedSalary,
                        BaseSalary = taskData.ChangeCompensation.BaseSalary,
                        businessIdentifier = taskData.ChangeCompensation.businessIdentifier,
                        CarBenefit = taskData.ChangeCompensation.CarBenefit,
                        costcenterIdentifier = taskData.ChangeCompensation.costcenterIdentifier,
                        currencyCode = taskData.ChangeCompensation.currencyCode,
                        EmissionBenefit = taskData.ChangeCompensation.EmissionBenefit,
                        EmployeeIdentifier = taskData.ChangeCompensation.EmployeeIdentifier,
                        EmployeeName = taskData.ChangeCompensation.EmployeeName,
                        HomeInternetBenefit = taskData.ChangeCompensation.HomeInternetBenefit,
                        InsuranceBenefit = taskData.ChangeCompensation.InsuranceBenefit,
                        NewTotalMonthlyPay = request.compensationTask.NewTotalMonthlyPay,
                        PhoneBenefit = taskData.ChangeCompensation.PhoneBenefit,
                        TotalMonthlyPay = taskData.ChangeCompensation.TotalMonthlyPay,
                        NewAdditionalAgreedPart = request.compensationTask.NewAdditionalAgreedPart
                    };
                }
                if (request.titleTask != null)
                {
                    task.ChangeTitle = new ChangeTitle()
                    {
                        businessIdentifier = taskData.ChangeTitle.businessIdentifier,
                        costcenterIdentifier = taskData.ChangeTitle.costcenterIdentifier,
                        EmployeeIdentifier = taskData.ChangeTitle.EmployeeIdentifier,
                        EmployeeName = taskData.ChangeTitle.EmployeeName,
                        NewTitle = request.titleTask.NewTitle,
                        oldTitle = taskData.ChangeTitle.EmployeeName
                    };
                }
            }
            else if (task.taskType == TaskType.Title && request.titleTask != null)
            {
                task.ChangeTitle = new ChangeTitle()
                {
                    businessIdentifier = taskData.ChangeTitle.businessIdentifier,
                    costcenterIdentifier = taskData.ChangeTitle.costcenterIdentifier,
                    EmployeeIdentifier = taskData.ChangeTitle.EmployeeIdentifier,
                    EmployeeName = taskData.ChangeTitle.EmployeeName,
                    NewTitle = request.titleTask.NewTitle,
                    oldTitle = taskData.ChangeTitle.oldTitle
                };
            }
            else if (task.taskType == TaskType.Team && taskData.ChangeTeam != null)
            {
                task.ChangeTeam = new ChangeTeam()
                {
                    managerIdentifier = taskData.ChangeTeam.managerIdentifier,
                    managerName = taskData.ChangeTeam.managerName,
                    newbusinessIdentifier = taskData.ChangeTeam.newbusinessIdentifier,
                    newCostCenterIdentifier = taskData.ChangeTeam.newCostCenterIdentifier,
                    newCostCenterName = taskData.ChangeTeam.newCostCenterName,
                    oldbusinessIdentifier = taskData.ChangeTeam.oldbusinessIdentifier,
                    oldCostCenterIdentifier  = taskData.ChangeTeam.oldCostCenterIdentifier, 
                    oldCostCenterName = taskData.ChangeTeam.oldCostCenterName,
                    organizationIdentifier = taskData.ChangeTeam.organizationIdentifier
                };
            }
            else if (task.taskType == TaskType.Bonus && request.bonusTask != null)
            {
                var b = _context.BonusSettings.FirstOrDefault(b => b.businessIdentifier == request.businessIdentifier);
                var bonus_amount = (request.bonusTask.one_time_bonus / taskData.ChangeBonus.salary) * 100;
                if (bonus_amount > 0
                    && b.limit_percentage < bonus_amount)
                {
                    throw new Exception("One time bonus amount cannot be more than limit");
                }
                task.ChangeBonus = new Database.Entities.BonusTask()
                {
                    salary = taskData.ChangeBonus.salary,
                    organizationIdentifier = taskData.ChangeBonus.organizationIdentifier,
                    businessIdentifier = taskData.ChangeBonus.businessIdentifier,
                    costcenterIdentifier = taskData.ChangeBonus.costcenterIdentifier,
                    EmployeeIdentifier = taskData.ChangeBonus.EmployeeIdentifier,
                    EmployeeName = taskData.ChangeBonus.EmployeeName,
                    one_time_bonus = request.bonusTask.one_time_bonus
                };
            }
            else if (task.taskType == TaskType.BudgetPromotion && request.budgetPromotionTask != null)
            {
                task.ChangeBudgetPromotion = _mapper.Map<UpdateBudgetPromotionTask, ChangeBudgetPromotion>(request.budgetPromotionTask);
            }
            else
            {
                throw new Exception("Detail object can not be empty");
            }
            return task;
        }
        private async Task SendNotification(CancellationToken cancellationToken, Notifications notification)
        {

            await _notificationRepository.AddNotifications(notification);
            await _context.SaveChangesAsync(cancellationToken);
            var connections = _userConnectionManager.GetUserConnections(notification.EmployeeId.ToString());
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", notification.title, notification.description,Enum.GetName(NotificationServiceType.TaskService), await _notificationRepository.CountUnReadNotifications(notification.EmployeeId));
                }
            }
        }

        //private async void PublishTask(TaskDto dto)
        //{
        //    if (dto.taskType == TaskType.Compensation && dto.compensationTask != null)
        //    {
        //        var Compensation = _mapper.Map<CompensationTask, CompensationEntity>(dto.compensationTask);
        //        await _messagePublisher.Publish(Compensation);
        //    }
        //    else if (dto.taskType == TaskType.Grade && dto.GradeTask != null)
        //    {
        //        var Grade = _mapper.Map<GradeTask, GradeEntity>(dto.GradeTask);
        //        await _messagePublisher.Publish(Grade);
        //    }
        //    else if (dto.taskType == TaskType.Holiday && dto.holidayTask != null)
        //    {
        //        var Holiday = _mapper.Map<HolidayTask, HolidayEntity>(dto.holidayTask);
        //        await _messagePublisher.Publish(Holiday);

        //    }
        //    else if (dto.taskType == TaskType.Manager && dto.managerTask != null)
        //    {
        //        var Manager = _mapper.Map<ManagerTask, ManagerEntity>(dto.managerTask);
        //        await _messagePublisher.Publish(Manager);
        //    }
        //    else if (dto.taskType == TaskType.Title && dto.titleTask != null)
        //    {
        //        var Title = _mapper.Map<TitleTask,TitleEntity>(dto.titleTask);
        //        await _messagePublisher.Publish(Title);
        //    }
        //}

    }
}
