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
using Organization.API.Repositories.Interface;

namespace Organization.API.Queries.GetOrganization
{
    public class GetManagerTree:IRequest<List<EmployeeResponse>>
    {
        public string Id { get; set; }
    }
    public class GetManagerTreeQueryHandler : IRequestHandler<GetManagerTree, List<EmployeeResponse>>
    {
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;

        public GetManagerTreeQueryHandler(IEmployee employee, IMapper mapper)
        {
            _employee = employee;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponse>> Handle(GetManagerTree request, CancellationToken cancellationToken)
        {
            try
            {
                //var o = await _context.Employees.FromSqlRaw($"sp_GetEmployeeTreeForManager {request.Id}").ToListAsync();
                var o = await _employee.GetEmployeeTreeForManager(request.Id);
                return _mapper.Map<List<Entities.Employee>, List<EmployeeResponse>>(o.ToList());
            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}
