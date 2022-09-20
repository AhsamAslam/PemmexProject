using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Database.Context;
using Organization.API.Database.Entities;
using Organization.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Queries.GetAllBusinessEmployees
{
    public class GetAllBusinessEmployeesQuery : IRequest<List<EmployeeResponse>>
    {
        public string Id { get; set; }
    }

    public class GetAllBusinessEmployeesQueryHandeler : IRequestHandler<GetAllBusinessEmployeesQuery, List<EmployeeResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetAllBusinessEmployeesQueryHandeler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<EmployeeResponse>> Handle(GetAllBusinessEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employee = await _context.Employees
                .Include(b => b.Businesses)
                .Where(e => e.Businesses.BusinessIdentifier == request.Id && e.IsActive == true)
                .Select(x => new EmployeeResponse
                {
                    EmployeeIdentifier = x.EmployeeIdentifier,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    ManagerIdentifier = x.ManagerIdentifier,
                    OrganizationIdentifier = x.Businesses.ParentBusinessId,
                    BusinessIdentifier = x.Businesses.BusinessIdentifier,
                    JobFunction = Enum.GetName(x.JobFunction),
                    Grade = x.Grade,
                    CostCenterIdentifier = x.CostCenter.CostCenterIdentifier,
                    CostCenterName = x.CostCenter.CostCenterName
                }).ToListAsync(cancellationToken);

                return employee;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
