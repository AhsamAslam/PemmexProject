using AutoMapper;
using MediatR;
using Organization.API.Database.Context;
using Organization.API.Database.Interfaces;
using Organization.API.Dtos;
using PemmexCommonLibs.Application.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetTeamMembers
{
    public class GetTeamMembersQuery : IRequest<List<EmployeeResponse>>
    {
        public string employeeIdentifier { get; set; }
    }
    public class GetTeamMembersQueryHandler : IRequestHandler<GetTeamMembersQuery, List<EmployeeResponse>>
    {
        private readonly IEmployeeRepository _context;
        private readonly IMapper _mapper;

        public GetTeamMembersQueryHandler(IEmployeeRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponse>> Handle(GetTeamMembersQuery request, CancellationToken cancellationToken)
        {
            var employees = await _context.GetTeamMembers(request.employeeIdentifier);
            return _mapper.Map<List<EmployeeResponse>>(employees);
        }
    }
}
