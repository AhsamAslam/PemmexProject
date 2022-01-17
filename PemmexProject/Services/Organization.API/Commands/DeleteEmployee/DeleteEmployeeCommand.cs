using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Entities;
using Organization.API.Interfaces;
using Organization.API.Repositories.Interface;
using PemmexCommonLibs.Application.Exceptions;

namespace Organization.API.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<int>
    {
        public Guid Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand,int>
    {
        private readonly IEmployee _employee;

        public DeleteEmployeeCommandHandler(IEmployee employee)
        {
            _employee = employee;
        }

        public async Task<int> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var entity = await _context.Employees.Where(x=>x.Emp_Guid == request.Id).FirstOrDefaultAsync(cancellationToken);
                var entity = await _employee.GetEmployeeByGuidId(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Employee), request.Id);
                }

                entity.IsActive = false;
                var employee = await _employee.UpdateEmployee(entity);
                return entity.EmployeeId;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}