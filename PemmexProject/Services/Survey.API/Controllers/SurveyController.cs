using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using PemmexCommonLibs.Application.Extensions;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using Survey.API.Commands.CreateEmployeeSurvey;
using Survey.API.Commands.CreateEmployeeSurvey.EmployeeSurveyRequests;
using Survey.API.Commands.CreateOrganizationSurvey;
using Survey.API.Commands.SaveEmployeeSurvey;
using Survey.API.Dto;
using Survey.API.Queries.GetEmployeeSurvey;
using Survey.API.Queries.GetOrganizationSurveyAverage;
using Survey.API.Queries.GetQuestionnaire;
using Survey.API.Queries.GetSurveyAverage;

namespace Survey.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SurveyController : ApiControllerBase
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IDateTime _dateTime;
        private readonly ILogService _logService;

        public SurveyController(IFileUploadService fileUploadService, IDateTime dateTime, ILogService logService)
        {
            _fileUploadService = fileUploadService;
            _dateTime = dateTime;
            _logService = logService;
        }
        [Authorize("GroupHR")]
        [HttpPost]
        public async Task<ActionResult<int>> PostAsync(OrganizationSurveyDto organizationSurvey)
        {
            try
            {
                var data = await Mediator.Send(new CreateOrganizationSurveyCommand { organizationSurvey = organizationSurvey });
                return data;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [Authorize("GroupHR")]
        [HttpPost]
        [Route("GenerateEmployeeSurvey")]
        public async Task<ActionResult<int>> GenerateEmployeeSurvey(List<GenerateEmployeeSurveyRequest> employeeSurvey)
        {
            try
            {
                var data = await Mediator.Send(new CreateEmployeeSurveyCommand { generateEmployeeSurveyRequest = employeeSurvey });
                return 1;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }

        [HttpGet]
        [Route("GetEmployeeSurvey")]
        public async Task<ActionResult<List<EmployeeSurveyDto>>> GetEmployeeSurvey()
        {
            try
            {
                return await Mediator.Send(new GetEmployeeSurveyQuery { employeeIdentifier = CurrentUser.EmployeeIdentifier, organizationIdentifier = CurrentUser.OrganizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpPost]
        [Route("UpdateEmployeeSurvey")]
        public async Task<ActionResult<int>> UpdateEmployeeSurvey(SaveEmployeeSurveyCommand command)
        {
            try
            {
                return await Mediator.Send(command);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
       
        [HttpPost]
        [Route("GetSurveyAverage")]
        public async Task<ActionResult<List<SurveySummaryDto>>> GetSurveyAverage([FromBody] List<string> employees)
        {
            try
            {
                return await Mediator.Send(new GetSurveyAverageQuery { employeeIdentifier = employees });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [Authorize("GroupHR")]
        [HttpGet]
        [Route("GetOrganizationSurveyAverage")]
        public async Task<ActionResult<List<SurveySummaryDto>>> GetOrganizationSurveyAverage(string organizationIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetOrganizationSurveyAverageCommand { organizationIdentifier = organizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [Authorize("GroupHR")]
        [HttpGet]
        [Route("GetQuestionnaire")]
        public async Task<List<SurveyQuestionsDto>> GetQuestionnaire()
        {
            try
            {
                return await Mediator.Send(new GetQuestionnaireQuery { });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
    }
}
