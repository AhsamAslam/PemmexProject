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
                var user = await _context.Users.Where(u => u.EmployeeIdentifier == request.Identifier && u.isActive == true).FirstOrDefaultAsync();
                return user;
            }
        }
    }
}