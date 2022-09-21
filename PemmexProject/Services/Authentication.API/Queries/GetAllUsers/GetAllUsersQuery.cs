using Authentication.API.Database.context;
using Authentication.API.Database.Entities;
using Authentication.API.Dtos;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
        public string Identifier { get; set; }
    }

    public class GetAllUsersQueryHandeler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {

            var o = from u in _context.Users
            join m in _context.Users on u.ManagerIdentifier equals m.EmployeeIdentifier into ps
            from p in ps.DefaultIfEmpty()
            select new UserDto
            {
                EmployeeIdentifier = u.EmployeeIdentifier,
                ManagerIdentifier = u.ManagerIdentifier,
                ManagerName = (p == null) ? string.Empty : (p.FirstName + " " + p.LastName),
                Grade = u.Grade,
                Email = u.Email,
                CostCenterIdentifier = u.CostCenterIdentifier,
                BusinessIdentifier = u.BusinessIdentifier,
                Id = u.Id,
                JobFunction = Enum.GetName(u.JobFunction),
                OrganizationCountry = u.OrganizationCountry,
                OrganizationIdentifier = u.OrganizationIdentifier,
                Role = u.Role,
                Title = u.Title,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName
            };

            return await o.ToListAsync();
        }
    }
}
