using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PemmexClient.Interfaces;
using PemmexClient.Models;
using PemmexCommonLibs.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
        [HttpGet]
        public async Task<ActionResult<ResponseMessage>> GetOrganization(int id)
        {
            return await _organizationService.GetOrganization(id);
        }
    }
}
