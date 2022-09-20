using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Extensions;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Commands.ApplyHoliday;

namespace TaskManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HolidayManager : ApiControllerBase
    {
        private readonly ILogService _logService;
        private readonly IFileUploadService _fileUploadService;
        private readonly IDateTime _dateTime;
        public HolidayManager(ILogService logService, 
            IFileUploadService fileUploadService, IDateTime dateTime)
        {
            _logService = logService;
            _fileUploadService = fileUploadService;
            _dateTime = dateTime;
        }
        [HttpPost]
        [Route("ApplyHoliday")]
        public async Task<ActionResult<ResponseMessage>> ApplyHoliday([FromForm]ApplyHolidayCommand command)
        {
            try
            {

                if(Request.Form.Files != null && Request.Form.Files.Count > 0 && command.holidayType == HolidayTypes.Sick)
                {
                    command.FileName = CurrentUser.Id + "_" +_dateTime.Now.FormatDateTime();
                    IFormFile file = Request.Form.Files[0];
                    await _fileUploadService.FileUploadToAzureAsync(file, command.FileName);
                }
                

                command.UserId = CurrentUser.Id;
                command.organizationIdentifier = CurrentUser.OrganizationIdentifier;
                command.businessIdentifier = CurrentUser.BusinessIdentifier;
                command.costcenterIdentifier = CurrentUser.CostCenterIdentifier;
                command.EmployeeIdentifier = CurrentUser.EmployeeIdentifier;
                command.ManagerIdentifier = CurrentUser.ManagerIdentifier;
                
               
                var data = await Mediator.Send(command);
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"HolidayManager_{CurrentUser.EmployeeIdentifier}");
                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
    }
}
