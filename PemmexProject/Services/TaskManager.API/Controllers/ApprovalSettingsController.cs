using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManager.API.Commands.SaveSetting;
using TaskManager.API.Dtos;
using TaskManager.API.Queries.GetApprovalSettings;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApprovalSettingsController : ApiControllerBase
    {
        private readonly ILogService _logService;
        public ApprovalSettingsController(ILogService logService)
        {
            _logService = logService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseMessage>> GetAsync()
        {
            try
            {
                var data = await Mediator.Send(new GetApprovalSettings { Id = CurrentUser.OrganizationIdentifier });
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e) 
            {
                await _logService.WriteLogAsync(e, $"ApprovalSettings_{CurrentUser.EmployeeIdentifier}");
                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }        
        }
        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> PostAsync(ApprovalSettingDto setting)
        {
            try
            {
                var data = await Mediator.Send(new SaveSettings { setting = setting });
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"ApprovalSettings_{CurrentUser.EmployeeIdentifier}");

                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
    }
}
