using Authentication.API.Database.context;
using Authentication.API.Database.Entities;
using Authentication.API.Database.Repositories.Interface;
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
        private readonly IUser _user; 
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandeler(IUser user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }
        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            List<UserDto> usersDto = new List<UserDto>();
            var users = await _user.GetAllUsers();
            foreach (var item in users)
            {
                usersDto = new List<UserDto>{
                    new UserDto
                    {
                         EmployeeIdentifier = item.EmployeeIdentifier,
                         ManagerIdentifier = item.ManagerIdentifier,
                         ManagerName = (item == null) ? string.Empty : (item.FirstName + " " + item.LastName),
                         Grade = item.Grade,
                         Email = item.Email,
                         CostCenterIdentifier = item.CostCenterIdentifier,
                         BusinessIdentifier = item.BusinessIdentifier,
                         Id = item.Id,
                         JobFunction = Enum.GetName(item.JobFunction),
                         OrganizationCountry = item.OrganizationCountry,
                         OrganizationIdentifier = item.OrganizationIdentifier,
                         Role = item.Role,
                         Title = item.Title,
                         UserName = item.UserName,
                         FirstName = item.FirstName,
                         LastName = item.LastName

                    }
                };
            }

            //var o = from u in _context.Users
            //join m in _context.Users on u.ManagerIdentifier equals m.EmployeeIdentifier into ps
            //from p in ps.DefaultIfEmpty()
            //select new UserDto
            //{
            //    EmployeeIdentifier = u.EmployeeIdentifier,
            //    ManagerIdentifier = u.ManagerIdentifier,
            //    ManagerName = (p == null) ? string.Empty : (p.FirstName + " " + p.LastName),
            //    Grade = u.Grade,
            //    Email = u.Email,
            //    CostCenterIdentifier = u.CostCenterIdentifier,
            //    BusinessIdentifier = u.BusinessIdentifier,
            //    Id = u.Id,
            //    JobFunction = Enum.GetName(u.JobFunction),
            //    OrganizationCountry = u.OrganizationCountry,
            //    OrganizationIdentifier = u.OrganizationIdentifier,
            //    Role = u.Role,
            //    Title = u.Title,
            //    UserName = u.UserName,
            //    FirstName = u.FirstName,
            //    LastName = u.LastName
            //};

            return usersDto.ToList();
        }
    }
}
