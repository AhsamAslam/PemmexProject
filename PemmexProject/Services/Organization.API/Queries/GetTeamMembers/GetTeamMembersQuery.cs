using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Interfaces;
using Organization.API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetTeamMembers
{
    public class GetTeamMembersQuery : IRequest<List<EmployeeResponse>>
    {
        public string costCenterIdentifier { get; set; }
    }
    public class GetTeamMembersQueryHandler : IRequestHandler<GetTeamMembersQuery, List<EmployeeResponse>>
    {
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;

        public GetTeamMembersQueryHandler(IEmployee employee, IMapper mapper)
        {
            _employee = employee;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponse>> Handle(GetTeamMembersQuery request, CancellationToken cancellationToken)
        {
            //var o = await _context.Employees
            //    .Include(b => b.Businesses)
            //    .Include(c => c.CostCenter)
            //    .Where(o => o.IsActive == true && o.CostCenter.CostCenterIdentifier == request.costCenterIdentifier)
            //    .ToListAsync(cancellationToken: cancellationToken);
            var o = await _employee.GetEmployeeByCostCenterIdentifier(request.costCenterIdentifier);

            return _mapper.Map<List<Entities.Employee>, List<EmployeeResponse>>(o.ToList());
        }
    }
}
