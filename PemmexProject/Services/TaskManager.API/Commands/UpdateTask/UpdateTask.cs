using AutoMapper;
using EventBus.Messages.Services;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
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
        public TaskReasons reasons { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string taskDescription { get; set; }
        public CompensationTask compensationTask { get; set; }
        public HolidayTask holidayTask { get; set; }
        public TitleTask titleTask { get; set; }
        public ManagerTask managerTask { get; set; }
        public GradeTask GradeTask { get; set; }
        public TeamTask TeamTask { get; set; }
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
                        t = PopulateDetailTask(t, request);
                        _context.BaseTasks.Update(t);
                        tasks.ForEach(t =>
                                t.currentTaskStatus = TaskStatuses.Approved);
                        _context.BaseTasks.UpdateRange(tasks);
                        await _context.SaveChangesAsync(cancellationToken);
                        if(t.taskType == TaskType.Holiday)
                        {
                            await SendTaskUpdation(t, request.userId, request.token, request.costcenterIdentifier);
                        }
                        else
                        {
                            await PublishOrganizationData(t);
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
                        t = PopulateDetailTask(t, request);
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
        private async Task SendNotification(CancellationToken cancellationToken, Notifications notification)
        {
            
            await _notificationRepository.AddNotifications(notification);
            await _context.SaveChangesAsync(cancellationToken);
            var connections = _userConnectionManager.GetUserConnections(notification.EmployeeId.ToString());
            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    await _notificationUserHubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", notification.title, notification.description, await _notificationRepository.CountUnReadNotifications(notification.EmployeeId));
                }
            }
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
        
        private async Task<HttpResponseMessage> SendTaskUpdation(BaseTask task,Guid? UserId,string token,string costcenterIdentifier)
        {
            try
            {
                var task_obj = await GetTaskTypeObject(task, UserId, costcenterIdentifier);
                var url = "";
                if (task.taskType == TaskType.Holiday)
                {
                    url = _configuration["HolidaysUrl"] + "/Holiday/SaveHolidays";

                   
                }
                using (var httpClient = new HttpClient())
                {
                    HttpRequestMessage requestMessage = new HttpRequestMessage(
                    HttpMethod.Post,
                    url);

                    httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
                    requestMessage.Content = new StringContent(JsonConvert.SerializeObject(task_obj), Encoding.UTF8, "application/json");

                    return await httpClient.SendAsync(requestMessage);

                }
            }
            catch(Exception)
            {
                throw;
            }
            
        }
        private async Task PublishOrganizationData(BaseTask task)
        {
            dynamic json = new ExpandoObject();
            OrganizationUpdateEntity entity = new OrganizationUpdateEntity();
            var taskData = await _context.BaseTasks
                    .Include(h => h.ChangeCompensation)
                    .Include(h => h.ChangeGrade)
                    .Include(h => h.ChangeManager)
                    .Include(h => h.ChangeTitle)
                    .Include(h => h.ChangeTeam)
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
                }
                else if (task.taskType == TaskType.Organization)
                {
                    if(task.ChangeGrade != null)
                    {
                        entity.Grade = task.ChangeGrade.newGrade;
                        entity.EmployeeIdentifier = taskData.ChangeGrade.EmployeeIdentifier;
                    }
                    if(task.ChangeManager != null)
                    {
                        entity.ManagerIdentifier = task.ChangeManager.newManagerIdentifier;
                        entity.CostCenterIdentifier = taskData.ChangeManager.newCostCenterIdentifier;
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
                    entity.ManagerIdentifier = task.ChangeTeam.managerIdentifier;
                    entity.CostCenterIdentifier = taskData.ChangeTeam.newCostCenterIdentifier;
                }
                entity.TaskType = taskData.taskType;
            }
            await _messagePublisher.Publish<OrganizationUpdateEntity>(entity);
        }
        private async Task<object> GetTaskTypeObject(BaseTask task,Guid? UserId,string costcenterIdentifier)
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
                    json.costcenterIdentifier = costcenterIdentifier;
                }
            }
            
            return json;
        }
        private BaseTask PopulateDetailTask(BaseTask task, UpdateTask request)
        {
            if (task.taskType == TaskType.Holiday && request.holidayTask != null)
            {
                //request.holidayTask.EmployeeIdentifier = request.requestIdentifier;
                task.ChangeHoliday = _mapper.Map<HolidayTask, ChangeHoliday>(request.holidayTask);
            }
            else if (task.taskType == TaskType.Compensation && request.compensationTask != null)
            {
                //request.compensationTask.EmployeeIdentifier = request.requestIdentifier;
                task.ChangeCompensation = _mapper.Map<CompensationTask, ChangeCompensation>(request.compensationTask);
            }
            else if (task.taskType == TaskType.Organization)
            {
                if(request.GradeTask != null)
                {
                    //request.GradeTask.EmployeeIdentifier = request.requestIdentifier;
                    task.ChangeGrade = _mapper.Map<GradeTask, ChangeGrade>(request.GradeTask);
                }
                if(request.managerTask != null)
                {
                    //request.managerTask.EmployeeIdentifier = request.requestIdentifier;
                    task.ChangeManager = _mapper.Map<ManagerTask, ChangeManager>(request.managerTask);
                }
                if (request.compensationTask != null)
                {
                    //request.compensationTask.EmployeeIdentifier = request.requestIdentifier;
                    task.ChangeCompensation = _mapper.Map<CompensationTask, ChangeCompensation>(request.compensationTask);
                }
                if (request.titleTask != null)
                {
                    //request.titleTask.EmployeeIdentifier = request.requestIdentifier;
                    task.ChangeTitle = _mapper.Map<TitleTask, ChangeTitle>(request.titleTask);
                }
            }
            else if (task.taskType == TaskType.Title && request.titleTask != null)
            {
                //request.titleTask.EmployeeIdentifier = request.requestIdentifier;
                task.ChangeTitle = _mapper.Map<TitleTask, ChangeTitle>(request.titleTask);
            }
            else if (task.taskType == TaskType.Team && request.TeamTask != null)
            {
                //request.titleTask.EmployeeIdentifier = request.requestIdentifier;
                task.ChangeTeam = _mapper.Map<TeamTask, ChangeTeam>(request.TeamTask);
            }
            else
            {
                throw new Exception("Detail object can not be empty");
            }
            return task;
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
