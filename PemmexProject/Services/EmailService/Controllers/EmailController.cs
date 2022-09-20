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
    [Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService mailService;
        private readonly ILogService _logService;
        public EmailController(IEmailService mailService, ILogService logService)
        {
            this.mailService = mailService;
            _logService = logService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromBody] EmailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                await _logService.WriteLogAsync(ex, $"Email Service_{request}");
                throw ex;
            }
        }
    }
}
