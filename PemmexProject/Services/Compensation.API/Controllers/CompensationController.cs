﻿using Compensation.API.Commands.SaveCompensation;
using Compensation.API.Dtos;
using Compensation.API.Queries.GetCompensation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compensation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize("ClientIdPolicy")]
    public class CompensationController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ResponseMessage>> Get(string EmployeeIdentifier)
        {
            try
            {
                var data = await Mediator.Send(new GetCompensationQuery { employeeIdentifier = EmployeeIdentifier });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
        [HttpPost]
        public async Task<ActionResult<ResponseMessage>> Post(CompensationDto compensation)
        {
            try
            {
                var data = await Mediator.Send(new SaveCompensationCommand { compensationDto = compensation });
                return new ResponseMessage(true, EResponse.OK, null, data);
            }
            catch (Exception e)
            {
                return new ResponseMessage(false, EResponse.UnexpectedError, e.Message, null);
            }
        }
    }
}