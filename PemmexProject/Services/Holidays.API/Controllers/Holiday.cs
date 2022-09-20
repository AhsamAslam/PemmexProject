using Holidays.API.Commands.SaveCalendar;
using Holidays.API.Commands.SaveHolidays;
using Holidays.API.Commands.SaveHolidaySetting;
using Holidays.API.Commands.SaveHolidaySettings;
using Holidays.API.Dtos;
using Holidays.API.Queries;
using Holidays.API.Queries.GetCountTakenHolidaysByEmployeeId;
using Holidays.API.Queries.GetEarnedHolidaysByEmployeeId;
using Holidays.API.Queries.GetHolidayCalendar;
using Holidays.API.Queries.GetHolidaySettings;
using Holidays.API.Queries.GetLeftHolidaysByBusinessId;
using Holidays.API.Queries.GetLeftHolidaysByEmployeeId;
using Holidays.API.Queries.GetPlannedHolidaysByEmployeeId;
using Holidays.API.Queries.GetSiblingsHolidays;
using Holidays.API.Queries.GetTakenHolidaysByTeamId;
using Holidays.API.Queries.GetTeamHeirarchyHolidays;
using Holidays.API.Queries.TeamHolidays;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using PemmexCommonLibs.Infrastructure.Services.FileUploadService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Holidays.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class Holiday : ApiControllerBase
    {
        private readonly ILogService _logService;

        public Holiday(ILogService logService)
        {
            _logService = logService;
        }
        [HttpGet]
        [Route("Types")]
        public async Task<ActionResult<ResponseMessage>> HolidayTypes()
        {
            try
            {
                string data = string.Join(",", Enum.GetNames(typeof(HolidayTypes)));
                return await Task.FromResult(new ResponseMessage(true, EResponse.OK, null, data));
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                return await Task.FromResult(new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null));
            }
        }
        
        [Authorize("BuHR")]
        [HttpPost]
        [Route("HolidaySettings")]
        public async Task<ActionResult<ResponseMessage>> SaveHolidaySettings(List<HolidaySettingsDto> settings)
        {
            try
            {
                var data = await Mediator.Send(new SaveHolidaySettingsCommand { settings = settings  });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [Authorize("BuHR")]
        [HttpPost]
        [Route("HolidaySetting")]
        public async Task<ActionResult<ResponseMessage>> SaveHolidaySetting(HolidaySettingsDto settings)
        {
            try
            {
                var data = await Mediator.Send(new SaveHolidaySettingCommand { setting = settings });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [Authorize("BuHR")]
        [HttpGet]
        [Route("HolidaySettings")]
        public async Task<ActionResult<ResponseMessage>> GetHolidaySettings()
        {
            try
            {
                var data = await Mediator.Send(new GetHolidaySettingsQuery { Id = CurrentUser.OrganizationIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        //[HttpPost]
        //[Route("SaveHolidays")]
        //public async Task<ActionResult<ResponseMessage>> SaveHolidays([FromBody] SaveHolidayCommand command)
        //{
        //    try
        //    {
        //        var data = await Mediator.Send(command);
        //        return new ResponseMessage(true, EResponse.OK, null, data);
        //    }
        //    catch (Exception e)
        //    {
        //        await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
        //        return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
        //    }
        //}
        [HttpGet]
        [Route("HolidayCounter")]
        public async Task<ActionResult<ResponseMessage>> HolidayCounter()
        {
            try
            {
                var data = await Mediator.Send(new GetHolidayCounterQuery() { businessIdentifier = CurrentUser.BusinessIdentifier, EmployeeIdentifier = CurrentUser.EmployeeIdentifier});
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("UserEarnedHolidays")]
        public async Task<ActionResult<int>> UserEarnedHolidays(string BusinessIdentifier, string EmployeeIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetEarnedHolidaysCountByEmployeeIdQuery() { businessIdentifier = BusinessIdentifier, Identifier = EmployeeIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("UserPlannedHolidays")]
        public async Task<ActionResult<int>> UserPlannedHolidays(string BusinessIdentifier,string EmployeeIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetPlannedHolidaysCountByEmployeeIdQuery() { businessIdentifier = BusinessIdentifier, EmployeeIdentifier = EmployeeIdentifier });
            }
            catch (Exception e) {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                throw;  
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("UserRemainingHolidays")]
        public async Task<ActionResult<int>> UserRemainingHolidays(string BusinessIdentifier, string EmployeeIdentifier, string country)
        {
            try
            {
                return await Mediator.Send(new GetLeftHolidaysCountByEmployeeIdQuery() { businessIdentifier = BusinessIdentifier, EmployeeIdentifier = EmployeeIdentifier, country = country });
            }
            catch (Exception e) {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}"); 
                throw; 
            }
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("UserAvailedHolidays")]
        public async Task<ActionResult<int>> UserAvailedHolidays(string BusinessIdentifier, string EmployeeIdentifier, string country)
        {
            try
            {
                return await Mediator.Send(new GetTakenHolidaysCountByEmployeeIdQuery() { businessIdentifier = BusinessIdentifier,
                    EmployeeIdentifier = EmployeeIdentifier,
                    country = country
                });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("SiblingsHolidays")]
        public async Task<ActionResult<List<EmployeeHolidayDto>>> SiblingsHolidays([FromQuery] string[] employeeIdentifiers)
        {
            try
            {
                return await Mediator.Send(new GetSiblingsHolidaysQuery() { employeeIdentifiers = employeeIdentifiers,organizationIdentifier = CurrentUser.OrganizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("TeamHeirarchyHolidays")]
        public async Task<ActionResult<List<EmployeeHolidayDto>>> TeamHeirarchyHolidays([FromQuery]string[] employeeIdentifiers)
        {
            try
            {
                return await Mediator.Send(new GetTeamHeirarchyHolidaysQuery() { employeeIdentifiers = employeeIdentifiers, organizationIdentifier = CurrentUser.OrganizationIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpPost]
        [Route("Calendar/{countryCode}")]
        public async Task<ActionResult<Unit>> CreateCalendar(string countryCode)
        {
            try
            {
                return await Mediator.Send(new CreateCalendarCommand() { countrycode = countryCode });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        //[HttpGet]
        //[Route("SubordinateHolidays")]
        //public async Task<ActionResult<ResponseMessage>> SubordinateHolidays()
        //{
        //    try
        //    {
        //        var data = await Mediator.Send(new SubordinateHolidaysQuery() { Id = CurrentUser.Id });
        //        return new ResponseMessage(true, EResponse.OK, null, data);
        //    }
        //    catch (Exception e)
        //    {
        //        return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
        //    }
        //}
        //[HttpGet]
        //[Route("TeamHolidaysCount")]
        //public async Task<ActionResult<ResponseMessage>> TeamHolidaysCount()
        //{
        //    try
        //    {
        //        var data = await Mediator.Send(new GetTeamHolidaysCountByBusinessId() { businessIdentifier = CurrentUser.BusinessIdentifier });
        //        return new ResponseMessage(true, EResponse.OK, null, data);
        //    }
        //    catch (Exception e)
        //    {
        //        return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
        //    }
        //}
        [AllowAnonymous]
        [HttpGet]
        [Route("BusinessEmployeesHolidays")]
        public async Task<ActionResult<List<EmployeeHolidaysCounter>>> BusinessEmployeesHolidays(string BusinessIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetHolidaysByBusinessIdQuery() { businessIdentifier = BusinessIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        [Route("Calendar/{countryCode}")]
        public async Task<ActionResult<ResponseMessage>> Calendar(string countryCode)
        {
            try
            {
                var data =  await Mediator.Send(new GetHolidayCalendarQuery() { countryName = countryCode });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Holiday_{CurrentUser.EmployeeIdentifier}");
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
    }
}
