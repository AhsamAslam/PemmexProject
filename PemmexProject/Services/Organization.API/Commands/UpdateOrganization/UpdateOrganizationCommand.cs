using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Organization.API.Entities;
using Organization.API.Interfaces;
using PemmexCommonLibs.Application.Exceptions;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Organization.API.Commands.UpdateOrganization
{
    public class UpdateOrganizationCommand : IRequest<int>
    {
        public string EmployeeIdentifier { get; set; }
        public string Title { get; set; }
        public string Grade { get; set; }
        public double AdditionalAgreedPart { get; set; }
        public double BaseSalary { get; set; }
        public DateTime EffectiveDate { get; set; }
        public string ManagerIdentifier { get; set; }
        public string CostCenterIdentifier { get; set; }
        public double CarBenefit { get; set; }
        public double InsuranceBenefit { get; set; }
        public double PhoneBenefit { get; set; }
        public double EmissionBenefit { get; set; }
        public double HomeInternetBenefit { get; set; }
        public double TotalMonthlyPay { get; set; }
        public TaskType TaskType { get; set; }
    }

    public class UpdateOrganizationCommandHandeler : IRequestHandler<UpdateOrganizationCommand, int>
    {
        private readonly IServiceProvider _serviceProvider;

        public UpdateOrganizationCommandHandeler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task<int> Handle(UpdateOrganizationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();
                    if (request.TaskType == TaskType.Team)
                    {
                        var employees = await _context.Employees
                            .Include(c => c.CostCenter)
                            .Where(e => (e.ManagerIdentifier == request.ManagerIdentifier
                        || e.EmployeeIdentifier == request.ManagerIdentifier)).ToListAsync();

                        var costCenter = await _context.CostCenters
                            .Where(c => c.CostCenterIdentifier == request.CostCenterIdentifier).FirstOrDefaultAsync();
                        employees.ForEach(e =>
                        {
                            e.CostCenter = costCenter;
                        });

                        _context.Employees.UpdateRange(employees);
                        await _context.SaveChangesAsync(cancellationToken);
                        return 1;
                    }
                    else
                    {
                        var e = await _context.Employees.Where(i => i.EmployeeIdentifier == request.EmployeeIdentifier)
                        .Include(e => e.CostCenter)
                        .FirstOrDefaultAsync(cancellationToken);
                        if (e == null)
                        {
                            throw new NotFoundException(nameof(Employee), request.EmployeeIdentifier);
                        }
                        if (!string.IsNullOrEmpty(request.Grade))
                        {
                            e.Grade = request.Grade;
                        }
                        if (!string.IsNullOrEmpty(request.Title))
                        {
                            e.Title = request.Title;
                        }
                        //if (request.BaseSalary > 0 || request.AdditionalAgreedPart > 0)
                        //{
                        //    Compensation compensation = new Compensation
                        //    {
                        //        EmployeeId = e.EmployeeId,
                        //        AdditionalAgreedPart = request.AdditionalAgreedPart,
                        //        BaseSalary = request.BaseSalary,
                        //        CarBenefit = request.CarBenefit,
                        //        EmissionBenefit = request.EmissionBenefit,
                        //        HomeInternetBenefit = request.HomeInternetBenefit,
                        //        InsuranceBenefit = request.InsuranceBenefit,
                        //        PhoneBenefit = request.PhoneBenefit,
                        //        EffectiveDate = request.EffectiveDate
                        //    };
                        //    e.Compensation.Add(compensation);
                        //}
                        if (!string.IsNullOrEmpty(request.ManagerIdentifier))
                        {
                            var employee = await _context.Employees.FirstOrDefaultAsync(e => e.ManagerIdentifier == request.ManagerIdentifier);
                            e.ManagerIdentifier = employee.ManagerIdentifier;
                            e.CostCenter = employee.CostCenter;
                        }
                        _context.Employees.Update(e);
                        await _context.SaveChangesAsync(cancellationToken);
                        return e.EmployeeId;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
