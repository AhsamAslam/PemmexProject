using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetOrganizationEmployees
{
    public class GetOrganizationEmployeesQuery : IRequest<List<EmployeeResponse>>
    {
        public string Id { get; set; }
    }

    public class GetAllOrganizationEmployeesQueryHandeler : IRequestHandler<GetOrganizationEmployeesQuery, List<EmployeeResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetAllOrganizationEmployeesQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<EmployeeResponse>> Handle(GetOrganizationEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await _context.Employees
                .Include(b => b.Businesses)
                .Include(c => c.CostCenter)
                .Where(e => e.Businesses.ParentBusinessId == request.Id && e.IsActive == true)
                .Select(x => new Employee { 
                    EmployeeIdentifier = x.EmployeeIdentifier,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ManagerIdentifier = x.ManagerIdentifier,
                    OrganizationIdentifier = x.Businesses.BusinessIdentifier,
                    JobFunction = x.JobFunction,
                    Grade = x.Grade,
                    CostCenter = new CostCenter
                    {
                        CostCenterIdentifier = x.CostCenter.CostCenterIdentifier,
                        CostCenterName = x.CostCenter.CostCenterName
                    }                    
                })
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<Employee>, List<EmployeeResponse>>(employees);
        }
    }
}
