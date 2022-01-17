using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Components;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;
using Organization.API.Repositories.Interface;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Infrastructure.Services.LogService;

namespace Organization.API.Commands.CreateEmployee
{
    public class CreateEmployeeCommand:IRequest<int> 
    {
        public EmployeeRequest employee { get; set; }
    }

    public class CreateEmployeeCommandHandeler : IRequestHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;
        public CreateEmployeeCommandHandeler(IEmployee employee,
            IMapper mapper)
        {
            _employee = employee;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var e = _mapper.Map<EmployeeRequest, Employee>(request.employee);
                e.IsActive = true;
                //_context.Employees.Add(e);
                //await _context.SaveChangesAsync(cancellationToken);
                var employee = await _employee.AddEmployee(e);
                return e.EmployeeId;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
