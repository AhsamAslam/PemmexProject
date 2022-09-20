using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organization.API.Commands.CreateCostCenter;
using Organization.API.Dtos;
using Organization.API.Queries.GetcostCenterIdentifier;
using Organization.API.Queries.GetcostCenterIdentifiers;
using Organization.API.Queries.GetcostCenterIdentifiersTree;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CostCentersController : ApiControllerBase
    {
        private readonly ILogService _logService;

        public CostCentersController(ILogService logService)
        {
            _logService = logService;
        }
        [HttpGet]
        [Route("{costCenterIdentifier}")]
        public async Task<ActionResult<CostCenterResponse>> GetAsync(string costCenterIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetcostCenterIdentifierQuery { costCenterIdentifier = costCenterIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"CostCenters_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<CostCenterResponse>>> Get()
        {
            try
            {
                return await Mediator.Send(new GetcostCenterIdentifiersQuery { businessIdentifier = CurrentUser.BusinessIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"CostCenters_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        
        [Authorize("Manager")]
        [HttpGet]
        [Route("Tree/{costIdentifier}")]
        public async Task<ActionResult<List<CostCenterResponse>>> Tree(string costIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetcostCenterIdentifiersTreeQuery { costCenterIdentifier = costIdentifier });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"CostCenters_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }
        [HttpPost]
        public async Task<ActionResult<int>> PostAsync(CostCenterRequest costCenterRequest)
        {
            try
            {
                return await Mediator.Send(new CreateCostCenterCommand { costCenterRequest = costCenterRequest });
            }
            catch (Exception e)
            {
                await _logService.WriteLogAsync(e, $"CostCenters_{CurrentUser.EmployeeIdentifier}");
                throw;
            }
        }


    }
}
