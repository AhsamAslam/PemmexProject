using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Database.Context;
using Organization.API.Database.Interfaces;
using Organization.API.Dtos;
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
        private readonly IOrganizationRepository _context;
        private readonly IMapper _mapper;

        public GetAllOrganizationEmployeesQueryHandeler(IOrganizationRepository context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<EmployeeResponse>> Handle(GetOrganizationEmployeesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var employees = await _context.GetOrganizationEmployees(request.Id);
                var e = _mapper.Map<List<EmployeeResponse>>(employees);
                return e;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
