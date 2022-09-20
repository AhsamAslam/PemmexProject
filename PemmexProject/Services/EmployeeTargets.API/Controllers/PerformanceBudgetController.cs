using EmployeeTargets.API.Commands.CreatePerformanceEvaluationSetting;
using EmployeeTargets.API.Commands.CreatePerformanceEvaluationSummary;
using EmployeeTargets.API.Commands.CreatePerfromanceBudgetPlanning;
using EmployeeTargets.API.Commands.DeletePerformanceEvaluationSetting;
using EmployeeTargets.API.Commands.UpdatePerformanceEvaluationSummaryIsActive;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Queries.GetBonusAmountByEmployeeIdentifier;
using EmployeeTargets.API.Queries.GetPerformanceEvaluationSettings;
using EmployeeTargets.API.Queries.GetPerformanceEvaluationSummary;
using EmployeeTargets.API.Queries.GetPerformanceEvaluationSummaryDetail;
using EmployeeTargets.API.Queries.GetPerfromanceBudgetPlanning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;

namespace EmployeeTargets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PerformanceBudgetController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public PerformanceBudgetController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> PostAsync(CreatePerfromanceBudgetPlanningCommand command)
        {
            try
            {
                
                var data = await Mediator.Send(command);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"PerformanceBudget_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

        [HttpGet]
        [Route("GetPerfromanceBudgetPlanning")]
        public async Task<ActionResult<PerfromanceBudgetPlanning>> GetPerfromanceBudgetPlanning([FromQuery] string organizationIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new GetPerfromanceBudgetPlanningQuery { organizationIdentifier = organizationIdentifier });
                return data;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"PerformanceBudget_{CurrentUser.EmployeeIdentifier}");
                throw e;
            }
        }
        
        [HttpPost]
        [Route("CreatePerformanceEvaluationSummary")]
        public async Task<ActionResult<ResponseMessage>> CreatePerformanceEvaluationSummary(List<CreatePerformanceEvaluationSummaryRequest> command)
        {
            try
            {
                var data = await Mediator.Send(new CreatePerformanceEvaluationSummaryCommand { performanceEvaluationSummaryRequest = command});
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"PerformanceBudget_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

        [HttpGet]
        [Route("GetPerformanceEvaluationSummary")]
        public async Task<ActionResult<List<PerformanceEvaluationSummary>>> GetPerformanceEvaluationSummary([FromQuery] string organizationIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new GetPerformanceEvaluationSummaryQuery { organizationIdentifier = organizationIdentifier });
                return data;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"PerformanceBudget_{CurrentUser.EmployeeIdentifier}");
                throw e;
            }
        }

        [HttpGet]
        [Route("GetPerformanceEvaluationSummaryDetail")]
        public async Task<ActionResult<List<PerformanceEvaluationSummary>>> GetPerformanceEvaluationSummaryDetail([FromQuery] string[] Identifiers)
        {
            try
            {
                
                var data = await Mediator.Send(new GetPerformanceEvaluationSummaryDetailQuery { employeeIdentifiers = Identifiers });
                return data;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"PerformanceBudget_{CurrentUser.EmployeeIdentifier}");
                throw e;
            }
        }
        [HttpPost]
        [Route("CreatePerformanceEvaluationSetting")]
        public async Task<ActionResult<ResponseMessage>> CreatePerformanceEvaluationSetting(CreatePerformanceEvaluationSettingCommand command)
        {
            try
            {
                command.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                var data = await Mediator.Send(command);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"PerformanceBudget_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

        [HttpDelete]
        [Route("DeletePerformanceEvaluationSetting/{Id}")]
        public async Task<ActionResult<ResponseMessage>> DeletePerformanceEvaluationSetting(int Id)
        {
            try
            {
                var data = await Mediator.Send(new DeletePerformanceEvaluationSettingCommand { Id = Id });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"PerformanceBudget_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

        [HttpGet]
        [Route("GetPerformanceEvaluationSettings")]
        public async Task<ActionResult<List<PerformanceEvaluationSettings>>> GetPerformanceEvaluationSettings()
        {
            try
            {

                var data = await Mediator.Send(new GetPerformanceEvaluationSettingsQuery { organizationIdentifier = CurrentUser.OrganizationIdentifier });
                return data;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"PerformanceBudget_{CurrentUser.EmployeeIdentifier}");
                throw e;
            }
        }

        [HttpGet]
        [Route("GetEmployeePerformanceBonus")]
        public async Task<ActionResult<ResponseMessage>> GetBonusAmountByEmployeeIdentifier()
        {
            try
            {

                var data = await Mediator.Send(new GetBonusAmountByEmployeeIdentifierQuery { employeeIdentifier = CurrentUser.EmployeeIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"PerformanceBudget_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

        [HttpPost]
        [Route("UpdateAllEmployeeTargetsIsActive")]
        public async Task<ActionResult<bool>> UpdateAllEmployeeTargetsIsActive([FromBody] List<string> Identifiers)
        {
            try
            {

                var data = await Mediator.Send(new UpdatePerformanceEvaluationSummaryIsActiveCommand { employeeIdentifier = Identifiers });
                return true;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"PerformanceBudget_{CurrentUser.EmployeeIdentifier}");
                throw e;
            }
        }
    }
}
