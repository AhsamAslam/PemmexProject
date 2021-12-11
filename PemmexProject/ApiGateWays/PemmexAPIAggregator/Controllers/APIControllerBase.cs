using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PemmexAPIAggregator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIControllerBase : ControllerBase
    {
        protected UserEntity CurrentUser => JsonConvert.DeserializeObject<UserEntity>(HttpContext.User.Claims.First(claim => claim.Type == "UserObject").Value);
    }
}
