using Authentication.API.Database.context;
using Authentication.API.Database.Entities;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PemmexCommonLibs.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Commands.SaveUser
{
    public class SaveUserCommand : IRequest
    {
        public User Users { get; set; }
    }

    public class SaveUserCommandHandeler : IRequestHandler<SaveUserCommand>
    {
        private readonly IServiceProvider _serviceProvider;
        public SaveUserCommandHandeler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<Unit> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {

            using (var scope = _serviceProvider.CreateScope())
            {
                var _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                request.Users.isActive = true;
                _context.Users.Add(request.Users);
                
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }

}
