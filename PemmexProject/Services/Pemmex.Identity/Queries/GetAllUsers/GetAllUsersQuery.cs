using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pemmex.Identity.Dtos;
using Pemmex.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pemmex.Identity.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<List<UserDto>>
    {
        public string Identifier { get; set; }
    }

    public class GetAllUsersQueryHandeler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandeler(IServiceProvider serviceProvider, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }
        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var Users = _userManager.Users
                                .Where(u => u.OrganizationIdentifier == request.Identifier)
                                .Include(u => u.UserRoles)
                                .ThenInclude(ur => ur.Role).ToList();
                List<UserDto> dtos = new List<UserDto>();
                foreach (var u in Users)
                {
                    UserDto dto = _mapper.Map<UserDto>(u);
                    var manager = Users.FirstOrDefault(p => p.EmployeeIdentifier == u.ManagerIdentifier);
                    if(manager != null)
                    dto.ManagerName = manager.FirstName + " " + manager.LastName;

                    foreach (var r in u.UserRoles)
                    {
                        dto.Role.Add(r.Role.Name);
                    }
                    dtos.Add(dto);
                }
                return dtos;
            }
        }
    }
}
