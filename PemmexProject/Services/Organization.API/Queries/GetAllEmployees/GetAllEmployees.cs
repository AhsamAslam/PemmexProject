using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Dtos;
using Organization.API.Interfaces;

namespace Organization.API.Queries.GetOrganization
{
    public class GetAllEmployees:IRequest<List<EmployeeResponse>>
    {
        public string Id { get; set; }
    }
    public class GetFullOrganizationQueryHandler : IRequestHandler<GetAllEmployees, List<EmployeeResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFullOrganizationQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponse>> Handle(GetAllEmployees request, CancellationToken cancellationToken)
        {

            var o = await _context.Employees.FromSqlRaw($"sp_GetEmployeeTreeForManager {request.Id}").ToListAsync();
            return _mapper.Map<List<Entities.Employee>, List<EmployeeResponse>>(o);
        }
    }
}
