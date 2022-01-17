using EmailServices.Dtos;
using EmailServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmailServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService mailService;
        private readonly ILogService _logService;

        public EmailController(IEmailService mailService, ILogService logService)
        {
            _logService = logService;
            this.mailService = mailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] EmailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"Email_");

                throw;
            }
        }
    }
}
