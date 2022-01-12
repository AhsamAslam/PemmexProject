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

namespace Organization.API.Queries.GetEmployeeByIdentifiers
{
    public class GetEmployeeByIdentifiersQuery : IRequest<List<EmployeeResponse>>
    {
        public string[] Identifiers { get; set; }
    }

    public class GetEmployeeByIdentifiersQueryHandeler : IRequestHandler<GetEmployeeByIdentifiersQuery, List<EmployeeResponse>>
    {
        
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;

        public GetEmployeeByIdentifiersQueryHandeler(IEmployee employee, IMapper mapper)
        {
           
            _employee = employee;
            _mapper = mapper;
        }
        public async Task<List<EmployeeResponse>> Handle(GetEmployeeByIdentifiersQuery request, CancellationToken cancellationToken)
        {
            //var employee = await _context.Employees
            //    .Where(e => request.Identifiers.Contains(e.EmployeeIdentifier) && e.IsActive == true)
            //    .Include(x => x.CostCenter)
            //    .Include(x => x.employeeContacts)
            //    .ToListAsync(cancellationToken);
            var employee = await _employee.GetEmployeeByEmployeeIdentifier(request.Identifiers);


            return _mapper.Map<List<Employee>, List<EmployeeResponse>>(employee.ToList());
        }
    }
}
