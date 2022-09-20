using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pemmex.Identity.Models;
using PemmexCommonLibs.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pemmex.Identity.Commands.UpdateTitle
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
                    var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var user = await _userManager.FindByIdAsync(request.EmployeeIdentifier);
                    if (user == null)
                    {
                        throw new NotFoundException(nameof(ApplicationUser), request.EmployeeIdentifier);
                    }

                    user.Title = request.Title;
                    user.Grade = request.Grade;
                    var a = await _userManager.UpdateAsync(user);
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
