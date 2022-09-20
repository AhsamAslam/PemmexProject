using Authentication.API.Configuration;
using Authentication.API.Database.context;
using Authentication.API.Database.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Queries.GetUserByName
{
    public class GetUserByIdentifierQueries : IRequest<User>
    {
        public string Identifier { get; set; }
    }

    public class GetUserByIdentifierQueryHandeler : IRequestHandler<GetUserByIdentifierQueries, User>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public GetUserByIdentifierQueryHandeler(IServiceProvider serviceProvider,IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }
        public async Task<User> Handle(GetUserByIdentifierQueries request, CancellationToken cancellationToken)
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
               IEnumerable<User> employees = (from e in _context.Users.Where(e1 => e1.EmployeeIdentifier == request.Identifier 
                                && e1.isActive == true)
                                join e1 in _context.Users on e.ManagerIdentifier
                                equals e1.EmployeeIdentifier into ps
                                from p in ps.DefaultIfEmpty()
                                select new User
                                {
                                    EmployeeIdentifier = e.EmployeeIdentifier,
                                    ManagerIdentifier = e.ManagerIdentifier,
                                    BusinessIdentifier = e.BusinessIdentifier,
                                    OrganizationIdentifier = e.OrganizationIdentifier,
                                    OrganizationCountry = e.OrganizationCountry,
                                    Email = e.Email,
                                    Grade = e.Grade,
                                    Id = e.Id,
                                    FirstName = e.FirstName,
                                    LastName = e.LastName,
                                    JobFunction = e.JobFunction,
                                    ManagerName = p == null ? "" : (p.FirstName +" "+ p.LastName),
                                    Title = e.Title,
                                    CostCenterIdentifier = e.CostCenterIdentifier,
                                    Role = e.Role,
                                    UserName = e.UserName,
                                    IsPasswordReset = e.IsPasswordReset
                                }).ToList();

                return employees.FirstOrDefault();
            }
        }
    }
}