using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Pemmex.Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pemmex.Identity.Commands.UpdateRole
{
    public class UpdateRoleCommand:IRequest
    {
        public string userId { get; set; }
        public string roleName { get; set; }
    }
    public class UpdateRoleCommandHandeler : IRequestHandler<UpdateRoleCommand>
    {
        private readonly IServiceProvider _serviceProvider;
        public UpdateRoleCommandHandeler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<Unit> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var user = await _userManager.FindByIdAsync(request.userId);
                    var roles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, roles);
                    await _userManager.AddToRoleAsync(user, request.roleName);
                 }
            }
            catch (Exception)
            {
                throw;
            }
            return Unit.Value;

        }
    }
}
