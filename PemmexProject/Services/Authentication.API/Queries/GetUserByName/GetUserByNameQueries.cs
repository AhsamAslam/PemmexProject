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
    public class GetUserByNameQueries : IRequest<User>
    {
        public string Name { get; set; }
    }

    public class GetUserByNameQueryHandeler : IRequestHandler<GetUserByNameQueries, User>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public GetUserByNameQueryHandeler(IServiceProvider serviceProvider, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }
        public async Task<User> Handle(GetUserByNameQueries request, CancellationToken cancellationToken)
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                var user = await _context.Users.Where(u => (u.UserName == request.Name || u.Email == request.Name) && u.isActive == true).FirstOrDefaultAsync();
                return user;
            }
        }
    }
}
