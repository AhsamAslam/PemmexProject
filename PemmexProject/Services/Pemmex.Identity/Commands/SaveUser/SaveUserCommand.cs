using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pemmex.Identity.Data;
using Pemmex.Identity.Models;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using PemmexCommonLibs.Infrastructure.Services.LogService;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pemmex.Identity.Commands.SaveUser
{
    public class SaveUserCommand : IRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string EmployeeIdentifier { get; set; }
        public string ManagerIdentifier { get; set; }
        public string ManagerName { get; set; }
        public string CostCenterIdentifier { get; set; }
        public string BusinessIdentifier { get; set; }
        public string OrganizationIdentifier { get; set; }
        public string UserName { get; set; }
        public bool isActive { get; set; }
        public string Grade { get; set; }
        public string OrganizationCountry { get; set; }
        public bool IsPasswordReset { get; set; }
        public JobFunction JobFunction { get; set; }
        public string Password { get; set; }
        public string[] Role { get; set; }
    }

    public class SaveUserCommandHandeler : IRequestHandler<SaveUserCommand>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public SaveUserCommandHandeler(IMapper mapper, IServiceProvider serviceProvider)
        {
            _mapper = mapper;
            _serviceProvider = serviceProvider;
        }
        public async Task<Unit> Handle(SaveUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                    var application_user = _mapper.Map<ApplicationUser>(request);
                    application_user.UserName = application_user.EmployeeIdentifier;                    
                    var p = await _userManager.CreateAsync(application_user, "t"+request.EmployeeIdentifier+"!");
                    if(p.Succeeded)
                    {
                        var role = await _userManager.AddToRoleAsync(application_user, request.Role[0]);
                    }
                }
            }
            catch (Exception e)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _logService = scope.ServiceProvider.GetRequiredService<LogService>();
                    await _logService.WriteLogAsync(e, $"{request.EmployeeIdentifier}-{request.Role}");
                }
            }
            return Unit.Value;

        }
    }
}
