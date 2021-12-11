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

            var o = await _context.Users
                .Where(o => o.OrganizationIdentifier == request.Identifier && o.isActive == true)
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<User>, List<UserDto>>(o);
        }
    }
}
