using EmployeeTargets.API.Commands.CreateSoftTarget;
using EmployeeTargets.API.Database.Entities;
using EmployeeTargets.API.Dtos;
using EmployeeTargets.API.Queries.ShowSoftTargets;
using EmployeeTargets.API.Queries.ShowSoftTargetsByUserId;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;

namespace EmployeeTargets.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftTargetsController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public SoftTargetsController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> PostAsync(SoftTargetsDto softDto)
        {
            try
            {
                var data = await Mediator.Send(new CreateSoftTargetCommand { SoftTargets = softDto});
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
        [Route("ShowSoftTargets")]
        public async Task<ActionResult<List<ShowSoftTargetsDto>>> GetSoftTarget([FromBody] string employeeIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new ShowSoftTargetQuery { employeeIdentifier = employeeIdentifier });
                return data;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw e;
            }
        }
        [HttpGet]
        [Route("ShowSoftTargetsByUserId")]
        public async Task<ActionResult<List<ShowSoftTargetsDto>>> ShowSoftTargetsByUserId()
        {
            try
            {
                var data = await Mediator.Send(new ShowSoftTargetsByUserIdQuery { userId = CurrentUser.Id.ToString(), employeeIdentifier = CurrentUser.EmployeeIdentifier });
                return data;
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Targets_{CurrentUser.EmployeeIdentifier}");
                throw e;
            }
        }
    }
}
