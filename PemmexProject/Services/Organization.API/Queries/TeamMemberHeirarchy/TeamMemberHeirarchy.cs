using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Organization.API.Database.Context;
using Organization.API.Database.Entities;
using Organization.API.Database.Interfaces;
using Organization.API.Dtos;
using PemmexCommonLibs.Domain.Common.Dtos;
using StackExchange.Redis;

namespace Organization.API.Queries.GetOrganization
{
    public class TeamMemberHeirarchy:IRequest<List<EmployeeResponse>>
    {
        public string Id { get; set; }
    }
    public class GetFullOrganizationQueryHandler : IRequestHandler<TeamMemberHeirarchy, List<EmployeeResponse>>
    {
        private readonly IEmployeeRepository _context;
        private readonly IMapper _mapper;
        private ConnectionMultiplexer _Connection = null;

        public GetFullOrganizationQueryHandler(IEmployeeRepository context,
            IConfiguration configuration,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _Connection = ConnectionMultiplexer.Connect(configuration["RedisConnectionStrings"]);


        }

        public async Task<List<EmployeeResponse>> Handle(TeamMemberHeirarchy request, CancellationToken cancellationToken)
        {
            var _redisCache = _Connection.GetDatabase();
            var employees = await _redisCache.StringGetAsync("Team_" + request.Id);
            var value = string.IsNullOrWhiteSpace(employees)
            ? default(List<RedisTeamDto>)
            : JsonConvert.DeserializeObject<List<RedisTeamDto>>(employees);

            if(employees.IsNull)
            {
                
                var o = await _context.GetTeamMembersHierarchy(request.Id);
                return _mapper.Map<List<EmployeeResponse>>(o);
            }
            else
            {
                var e = value.Select(e => e.EmployeeIdentifier).ToArray();
                var emp = await _context.GetEmployees(e);
                return _mapper.Map<List<EmployeeResponse>>(emp);
            }
            
        }
    }
}
