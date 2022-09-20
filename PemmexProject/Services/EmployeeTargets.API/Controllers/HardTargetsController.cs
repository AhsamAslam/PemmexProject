using EmployeeTargets.API.Commands.CreateHardTarget;
using EmployeeTargets.API.Commands.CreateSoftTarget;
using EmployeeTargets.API.Commands.UpdateHardTargetsMeasurementCriteriaResult;
using EmployeeTargets.API.Commands.UpdateHardTargetsWeightage;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Dtos;
using EmployeeTargets.API.Queries.ShowHardTargets;
using EmployeeTargets.API.Queries.ShowHardTargetsByUserId;
using EmployeeTargets.API.Queries.ShowSoftTargets;
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
    public class HardTargetsController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public HardTargetsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostAsync(HardTargetsDto hardDto)
        {
            try
            {
                var data = await Mediator.Send(new CreateHardTargetCommand { HardTargets = hardDto});
                return true;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw e;
                //return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }


        [HttpGet]
        [Route("ShowHardTargets")]
        public async Task<ActionResult<List<ShowHardTargetsDto>>> GetHardTarget([FromQuery] string employeeIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new ShowHardTargetsQuery { employeeIdentifier = employeeIdentifier });
                return data;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw e;
            }
        }

        [HttpGet]
        [Route("ShowHardTargetsByUserId")]
        public async Task<ActionResult<List<ShowHardTargetsDto>>> ShowHardTargetsByUserId()
        {
            try
            {
                var data = await Mediator.Send(new ShowHardTargetsByUserIdQuery { userId = CurrentUser.Id.ToString(), employeeIdentifier = CurrentUser.EmployeeIdentifier });
                return data;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw e;
            }
        }

        [HttpPost]
        [Route("UpdateHardTargetsMeasurementCriteriaResult")]
        public async Task<ActionResult<ResponseMessage>> UpdateHardTargetsMeasurementCriteriaResult(UpdateHardTargetsMeasurementCriteriaResultCommand command)
        {
            try
            {
                var data = await Mediator.Send(command);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }

        [HttpPost]
        [Route("UpdateHardTargetsWeightage")]
        public async Task<ActionResult<ResponseMessage>> UpdateHardTargetsWeightage(UpdateHardTargetsWeightageCommand command)
        {
            try
            {
                var data = await Mediator.Send(command);
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
    }
}
