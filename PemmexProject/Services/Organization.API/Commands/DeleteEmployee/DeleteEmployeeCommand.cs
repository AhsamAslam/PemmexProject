using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Entities;
using Organization.API.Interfaces;
using PemmexCommonLibs.Application.Exceptions;

namespace Organization.API.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest
    {
        public string Id { get; set; }
    }

    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteEmployeeCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var guid = Guid.Parse(request.Id);
                var entity = await _context.Employees.Where(x=>x.Emp_Guid == guid).FirstOrDefaultAsync(cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Employee), request.Id);
                }

                entity.IsActive = false;

                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
            return Unit.Value;
        }
    }
}