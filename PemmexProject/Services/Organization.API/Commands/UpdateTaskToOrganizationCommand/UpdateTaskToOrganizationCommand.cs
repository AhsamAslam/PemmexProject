using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Organization.API.Database.Context;
using PemmexCommonLibs.Domain.Common.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Commands.UpdateTaskToOrganizationCommand
{
    public class UpdateTaskToOrganizationCommand : IRequest
    {
        public TaskEntity task { get; set; }
    }

    public class UpdateTaskToOrganizationCommandHandeler : IRequestHandler<UpdateTaskToOrganizationCommand>
    {
        private readonly IServiceProvider _serviceProvider;
        public UpdateTaskToOrganizationCommandHandeler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<Unit> Handle(UpdateTaskToOrganizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                    //request.Users.isActive = true;
                    //_context.Users.Add(request.Users);

                    await _context.SaveChangesAsync(cancellationToken);
                }
                return Unit.Value;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
