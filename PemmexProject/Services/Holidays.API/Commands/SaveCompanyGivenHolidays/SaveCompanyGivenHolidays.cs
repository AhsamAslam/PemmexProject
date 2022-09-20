using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PemmexCommonLibs.Infrastructure.Services.LogService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Commands.SaveHolidays
{
    public class SaveCompanyGivenHolidays : IRequest
    {
        public CompanyToEmployeeHolidays companyHolidays { get; set; }
    }

    public class SaveUserCommandHandeler : IRequestHandler<SaveCompanyGivenHolidays>
    {
        private readonly IServiceProvider _serviceProvider;
        public SaveUserCommandHandeler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<Unit> Handle(SaveCompanyGivenHolidays request, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                    _context.CompanyToEmployeeHolidays.Add(request.companyHolidays);
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            catch(Exception e)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _logService = scope.ServiceProvider.GetRequiredService<LogService>();
                    await _logService.WriteLogAsync(e, $"{request.companyHolidays.EmployeeIdentifier}");
                }
            }
            return Unit.Value;
        }
    }
}
