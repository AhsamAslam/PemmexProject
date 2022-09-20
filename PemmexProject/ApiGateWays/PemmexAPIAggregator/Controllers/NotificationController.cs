using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Services;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationController : APIControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IOrganizationService _organizationService;
        private readonly ICompensationService _compensationService;

        public NotificationController(INotificationService notificationService,
            IOrganizationService organizationService,
            ICompensationService compensationService)
        {
            _notificationService = notificationService;
            _organizationService = organizationService;
            _compensationService = compensationService;
        }

        [HttpPost]
        [Route("PostNotification")]
        public async Task<string> Post()
        {
            try
            {
                var managers = await _organizationService.GetManagers();

                string notification = "";
                Parallel.ForEach(managers, async manager =>
                {
                    dynamic json = new ExpandoObject();
                    json.userId = manager.EmployeeIdentifier;
                    json.isRead = false;
                    json.taskType = TaskType.AnnualSalary;
                    json.description = "AnnualSalary";
                    json.title = "AnnualSalary";


                    notification = await _notificationService.PostNotification(JsonConvert.SerializeObject(json));
                });
                return "Notification send..";
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
