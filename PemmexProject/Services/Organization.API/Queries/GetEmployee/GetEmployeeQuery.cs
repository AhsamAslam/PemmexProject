using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;
using Organization.API.Repositories.Interface;

namespace Organization.API.Queries.GetEmployee
{
    public class GetEmployeeQuery:IRequest<EmployeeResponse>
    {
        public string Id { get; set; }
    }

    public class GetEmployeeQueryHandeler : IRequestHandler<GetEmployeeQuery, EmployeeResponse>
    {
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandeler(IEmployee employee, IMapper mapper)
        {
            _employee = employee;
            _mapper = mapper;
        }
        public async Task<EmployeeResponse> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Guid guidOutput;
                bool isValid = Guid.TryParse(request.Id, out guidOutput);
                if (isValid)
                {
                    //var employee = await _context.Employees
                    //.Where(e => e.Emp_Guid == guidOutput && e.IsActive == true)
                    //.Include(x => x.CostCenter)
                    //.Include(x => x.employeeContacts)
                    //.FirstOrDefaultAsync(cancellationToken);
                    var employee = await _employee.GetEmployeeByEmpGuid(guidOutput);
                    return _mapper.Map<Employee, EmployeeResponse>(employee);

                }
                else
                {
                    //var employee = await _context.Employees
                    //.Where(e => e.EmployeeIdentifier == request.Id && e.IsActive == true)
                    //.Include(x => x.CostCenter)
                    //.Include(x => x.employeeContacts)
                    //.FirstOrDefaultAsync(cancellationToken);
                    var employee = await _employee.GetEmployeeByEmployeeIdentifier(request.Id);
                    return _mapper.Map<Employee, EmployeeResponse>(employee);
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
