using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pemmex.Identity.Data;
using Pemmex.Identity.Models;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Pemmex.Identity.Commands.AddUserToBusinessUnit
{
    public class AddUserToBusinessUnitCommand : IRequest
    {
        public string employeeIdentifier { get; set; }
        public string businessUnitIdentifier { get; set; }
    }
    public class AddUserToBusinessUnitCommandHandeler : IRequestHandler<AddUserToBusinessUnitCommand>
    {
        private readonly IServiceProvider _serviceProvider;
        public AddUserToBusinessUnitCommandHandeler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<Unit> Handle(AddUserToBusinessUnitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var user = await _context.Users
                        .Where(u => u.EmployeeIdentifier == request.employeeIdentifier)
                        .Include(b => b.UserBusinessUnits)
                        .FirstOrDefaultAsync();

                    var b_unit = user.UserBusinessUnits
                        .FirstOrDefault(b => b.BUnitIdentifier == request.businessUnitIdentifier);

                    if(b_unit != null)
                    {
                        throw new Exception("Record already Exists");
                    }
                    var roles = await _context.UserRoles
                        .Where(r => r.UserId == user.Id)
                        .Select(u => u.Role.Name)
                        .ToListAsync();

                    if(roles.Contains(Enum.GetName(Roles.buhr)))
                    {
                        user.UserBusinessUnits.Add(new UserBusinessUnits()
                        {
                            BUnitIdentifier = request.businessUnitIdentifier
                        });
                        await _context.SaveChangesAsync(cancellationToken);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException("User does not have BusinessUnit HR Rights");
                    }
                   
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
