using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organization.API.Commands.CreateCostCenter;
using Organization.API.Dtos;
using Organization.API.Queries.GetcostCenterIdentifier;
using Organization.API.Queries.GetcostCenterIdentifiers;
using Organization.API.Queries.GetcostCenterIdentifiersTree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Organization.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostCentersController : ApiControllerBase
    {
        [HttpGet]
        [Route("{costCenterIdentifier}")]
        public async Task<ActionResult<CostCenterResponse>> GetAsync(string costCenterIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetcostCenterIdentifierQuery { costCenterIdentifier = costCenterIdentifier });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public async Task<ActionResult<List<CostCenterResponse>>> Get(string businessIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetcostCenterIdentifiersQuery { organizationIdentifier = businessIdentifier });
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        [Route("Tree/{costIdentifier}")]
        public async Task<ActionResult<List<CostCenterResponse>>> Tree(string costIdentifier)
        {
            try
            {
                return await Mediator.Send(new GetcostCenterIdentifiersTreeQuery { costCenterIdentifier = costIdentifier });
            }
            catch (Exception)
            {
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
            catch (Exception)
            {
                throw;
            }
        }


    }
}
