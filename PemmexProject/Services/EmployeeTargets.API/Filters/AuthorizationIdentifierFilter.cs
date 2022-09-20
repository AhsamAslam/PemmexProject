using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using StackExchange.Redis;
using System.Net;

namespace EmployeeTargets.API.Filters
{
    public class AuthorizationIdentifierFilter : IActionFilter
    {
        private ConnectionMultiplexer _Connection = null;
        public AuthorizationIdentifierFilter(IConfiguration configuration)
        {
            _Connection = ConnectionMultiplexer.Connect(configuration["RedisConnectionStrings"]);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public async void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var parameter in context.ActionArguments)
            {
                var identifiers = (string[])parameter.Value;
                var user = JsonConvert.DeserializeObject<UserEntity>(context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserObject")?.Value);
                if (identifiers[0] == user.EmployeeIdentifier)
                {

                }
                else if (user.Role.Contains(Enum.GetName(Roles.buhr)))
                {
                    var _redisCache = _Connection.GetDatabase();
                    var employees = await _redisCache.StringGetAsync("BUnit_" + user.EmployeeIdentifier);
                    var value = string.IsNullOrWhiteSpace(employees)
                    ? default(List<RedisTeamDto>)
                    : JsonConvert.DeserializeObject<List<RedisTeamDto>>(employees);

                    if (employees.IsNull)
                    {
                        context.Result = new BadRequestObjectResult(HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        var authorized = value.Select(v => v.EmployeeIdentifier).ToArray();
                        foreach (var i in identifiers)
                        {
                            if (!authorized.Contains(i))
                            {
                                context.Result = new BadRequestObjectResult(HttpStatusCode.Unauthorized + $" Access for Identifier {i} not allowed.");
                            }
                        }
                    }
                }
                else if (user.Role.Contains(Enum.GetName(Roles.manager)))
                {
                    var _redisCache = _Connection.GetDatabase();
                    var employees = await _redisCache.StringGetAsync("Team_" + user.EmployeeIdentifier);
                    var value = string.IsNullOrWhiteSpace(employees)
                    ? default(List<RedisTeamDto>)
                    : JsonConvert.DeserializeObject<List<RedisTeamDto>>(employees);

                    if (employees.IsNull)
                    {
                        context.Result = new BadRequestObjectResult(HttpStatusCode.Unauthorized);
                    }
                    else
                    {
                        var authorized = value.Select(v => v.EmployeeIdentifier).ToArray();
                        foreach (var i in identifiers)
                        {
                            if (!authorized.Contains(i))
                            {
                                context.Result = new BadRequestObjectResult(HttpStatusCode.Unauthorized + $" Access for Identifier {i} not allowed.");
                            }
                        }
                    }
                }
                else if (user.Role.Contains(Enum.GetName(Roles.user)))
                {
                    if (identifiers[0] != user.EmployeeIdentifier)
                    {
                        context.Result = new BadRequestObjectResult(HttpStatusCode.Unauthorized);
                    }
                }
                Console.Write(string.Format("{0}: {1}", parameter.Key, parameter.Value));
            }
        }
    }
}
