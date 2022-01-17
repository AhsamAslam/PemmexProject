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

namespace Organization.API.Queries.GetAllBusinessEmployees
{
    public class GetAllBusinessEmployeesQuery : IRequest<List<EmployeeResponse>>
    {
        public string Id { get; set; }
    }

    public class GetAllBusinessEmployeesQueryHandeler : IRequestHandler<GetAllBusinessEmployeesQuery, List<EmployeeResponse>>
    {
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;

        public GetAllBusinessEmployeesQueryHandeler( IEmployee employee, IMapper mapper)
        {
            _employee = employee;
            _mapper = mapper;
        }
        public async Task<List<EmployeeResponse>> Handle(GetAllBusinessEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                //var employee = await _context.Employees
                //    .Include(b => b.Businesses)
                //    .Where(e => e.Businesses.BusinessIdentifier == request.Id && e.IsActive == true)
                //    .ToListAsync(cancellationToken);
                var employee = await _employee.GetEmployeeByBusinessIdentifier(request.Id);

                return _mapper.Map<List<Employee>, List<EmployeeResponse>>(employee.ToList());
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
