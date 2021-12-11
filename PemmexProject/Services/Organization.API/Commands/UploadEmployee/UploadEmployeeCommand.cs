using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;

namespace Organization.API.Commands.UploadEmployees
{
    public class UploadEmployeeCommand:IRequest<List<Employee>>
    {
        public List<EmployeeUploadRequest> employees { get; set; }
    }

    public class UploadEmployeeCommandHandeler : IRequestHandler<UploadEmployeeCommand,List<Employee>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UploadEmployeeCommandHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<Employee>> Handle(UploadEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var o = _mapper.Map<List<EmployeeUploadRequest>, List<Employee>>(request.employees);
                o.ForEach(x => x.IsActive = true);
                _context.Employees.AddRange(o);
                await _context.SaveChangesAsync(cancellationToken);
                return o;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
