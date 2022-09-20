using Authentication.API.Database.context;
using Authentication.API.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PemmexCommonLibs.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Authentication.API.Commands.UpdateTitle
{
    public class UpdateTitleCommand : IRequest
    {
        public string EmployeeIdentifier { get; set; }
        public string Title { get; set; }
        public string Grade { get; set; }
    }

    public class UpdateOrganizationCommandHandeler : IRequestHandler<UpdateTitleCommand>
    {
        private readonly IServiceProvider _serviceProvider;
        public UpdateOrganizationCommandHandeler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<Unit> Handle(UpdateTitleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                    var e = await _context.Users.Where(i => i.EmployeeIdentifier == request.EmployeeIdentifier)
                    .FirstOrDefaultAsync(cancellationToken);
                    if (e == null)
                    {
                        throw new NotFoundException(nameof(User), request.EmployeeIdentifier);
                    }

                    e.Title = request.Title;
                    e.Grade = request.Grade;
                    _context.Users.Update(e);
                    var a = await _context.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
