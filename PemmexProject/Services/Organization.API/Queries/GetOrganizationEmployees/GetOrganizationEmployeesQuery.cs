using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;
using Organization.API.Repositories.Interface;
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
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;

        public GetAllOrganizationEmployeesQueryHandeler(IApplicationDbContext context, IEmployee employee, IMapper mapper)
        {
            _context = context;
            _employee = employee;
            _mapper = mapper;
        }
        public async Task<List<EmployeeResponse>> Handle(GetOrganizationEmployeesQuery request, CancellationToken cancellationToken)
        {
            //var employees = await _context.Employees
            //    .Include(b => b.Businesses)
            //    .Include(c => c.CostCenter)
            //    .Where(e => e.Businesses.ParentBusinessId == request.Id && e.IsActive == true)
            //    .Select(x => new Employee { 
            //        EmployeeIdentifier = x.EmployeeIdentifier,
            //        FirstName = x.FirstName,
            //        LastName = x.LastName,
            //        ManagerIdentifier = x.ManagerIdentifier,
            //        OrganizationIdentifier = x.Businesses.BusinessIdentifier,
            //        JobFunction = x.JobFunction,
            //        Grade = x.Grade,
            //        CostCenter = new CostCenter
            //        {
            //            CostCenterIdentifier = x.CostCenter.CostCenterIdentifier,
            //            CostCenterName = x.CostCenter.CostCenterName
            //        }                    
            //    })
            //    .ToListAsync(cancellationToken);
            List<Employee> employees = new List<Employee>();
            var employeeList = await _employee.GetEmployeeAndBusinessAndCostCenterByParentBusinessId(request.Id);
            foreach (var item in employeeList)
            {
                Employee emp = new Employee();
                emp.EmployeeIdentifier = item.EmployeeIdentifier;
                emp.FirstName = item.FirstName;
                emp.LastName = item.LastName;
                emp.ManagerIdentifier = item.ManagerIdentifier;
                emp.OrganizationIdentifier = item.OrganizationIdentifier;
                emp.JobFunction = item.JobFunction;
                emp.Grade = item.Grade;
                emp.CostCenter = new CostCenter
                {
                    CostCenterIdentifier = item.CostCenter.CostCenterIdentifier,
                    CostCenterName = item.CostCenter.CostCenterName
                };
                employees.Add(emp);

            }

            return _mapper.Map<List<Employee>, List<EmployeeResponse>>(employees);
        }
    }
}
