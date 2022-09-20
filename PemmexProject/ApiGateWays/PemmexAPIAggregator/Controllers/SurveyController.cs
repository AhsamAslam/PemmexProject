using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PemmexAPIAggregator.Models;
using PemmexAPIAggregator.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using PemmexCommonLibs.Domain.Enums;
using System.Linq;

namespace PemmexAPIAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SurveyController : APIControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IOrganizationService _organizationService;
        private readonly ISurveyService _surveyService;

        public SurveyController(INotificationService notificationService,
            IOrganizationService organizationService, ISurveyService surveyService)
        {
            _notificationService = notificationService;
            _organizationService = organizationService;
            _surveyService = surveyService;
        }

        [HttpPost]
        [Route("CreateOrganizationSurvey")]
        [Authorize("GroupHR")]
        public async Task<int> Post(OrganizationSurveyDto survey)
        {
            try
            {
                var Id = await _surveyService.CreateOrganizationSurvey(JsonConvert.SerializeObject(survey));

                var Employee = await _organizationService.GetOrganizationEmployees(CurrentUser.OrganizationIdentifier);

                dynamic announceNotificationCommand = new ExpandoObject();
                dynamic employeeSurvey = new List<ExpandoObject>();
                List<string> userIds = new List<string>();

                foreach (var item in Employee)
                {
                    userIds.Add(item.EmployeeIdentifier);
                    
                        dynamic obj = new ExpandoObject();
                        obj.employeeIdentifier = item.EmployeeIdentifier;
                        obj.employeeName = item.FirstName + " " + item.LastName;
                        obj.managerIdentifier = item.ManagerIdentifier;
                        obj.managerName = item.ManagerName == null ? "" : item.ManagerName ;
                        obj.businessIdentifier = item.BusinessIdentifier;
                        obj.surveyRate = SurveyRate.Zero;
                        obj.organizationSurveyId = Convert.ToInt32(Id);
                          
                        employeeSurvey.Add(obj);
                    //obj.segmentId = 
                    
                }

                announceNotificationCommand.userId = userIds;
                announceNotificationCommand.isRead = false;
                announceNotificationCommand.taskType = 0;
                announceNotificationCommand.description = "Abcd";
                announceNotificationCommand.title = "Survey";


                var Notification = await _notificationService.PostAnnounceNotification(JsonConvert.SerializeObject(announceNotificationCommand));
                var EmployeeSurveys = await _surveyService.GenerateEmployeeSurvey(JsonConvert.SerializeObject(employeeSurvey));
                
                return 1;
            }
            catch (System.Exception)
            {

                throw;
            }
        }


        [HttpPost]
        [Route("GetSurveyAverage")]
        [Authorize("manager")]
        public async Task<List<SurveySummaryDto>> GetSurveyAverage(string employeeIdentifier)
        {
            try
            {
                var role = CurrentUser.Role;
                List<SurveySummaryDto> surveySummary = new List<SurveySummaryDto>();
                var teamMembers = await _organizationService.GetTeamMembers(employeeIdentifier);
                List<string> employeeIdentifiers = new List<string>();
                foreach (var item in teamMembers)
                {
                    employeeIdentifiers.Add(item.EmployeeIdentifier);
                }
                if(role[0] == "manager")
                    surveySummary = await _surveyService.GetSurveyAverage(JsonConvert.SerializeObject(employeeIdentifiers));
                else if(role[0] == "grouphr")
                    surveySummary = await _surveyService.GetOrganizationSurveyAverage(JsonConvert.SerializeObject(CurrentUser.OrganizationIdentifier));

                return surveySummary;
            }
            catch (Exception e )
            {

                throw;
            }
        }
    }
}
