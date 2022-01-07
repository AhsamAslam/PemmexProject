using AutoMapper;
using Compensation.API.Database.context;
using Compensation.API.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PemmexCommonLibs.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Compensation.API.Commands.SaveSalary
{
    public class SaveSalaryCommand : IRequest
    {
        public string EmployeeIdentifier { get; set; }
    }

    public class SaveSalaryCommandHandeler : IRequestHandler<SaveSalaryCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        public SaveSalaryCommandHandeler(IApplicationDbContext context, 
            IDateTime dateTime)
        {
            _context = context;
            _dateTime = dateTime;
        }
        public async Task<Unit> Handle(SaveSalaryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var c = await _context.Compensation
                    .Where(e => e.EmployeeIdentifier == request.EmployeeIdentifier)
                    .OrderByDescending(c => c.EffectiveDate).Take(2)
                    .ToListAsync();
                if(c.Count > 1) 
                {
                    var com1 = c.Take(1).FirstOrDefault();
                    var com2 = c.Take(2).FirstOrDefault();
                    if(com1.EffectiveDate.Year == _dateTime.Now.Year
                        && com1.EffectiveDate.Month == _dateTime.Now.Month)
                    {
                        var prev_days = (com1.EffectiveDate -
                        new DateTime(com1.EffectiveDate.Year, com1.EffectiveDate.Month, 1)).TotalDays;
                        var month_days = _dateTime.DaysInMonth;
                        var cur_days = month_days - prev_days;
                        
                        var salary = await _context.CompensationSalaries
                        .FirstOrDefaultAsync(c => c.EmployeeIdentifier == request.EmployeeIdentifier
                        && (c.IssuedDate.Year == _dateTime.Now.Year
                        && c.IssuedDate.Month == _dateTime.Now.Month));
                        if (salary == null)
                        {

                            CompensationSalaries s = new CompensationSalaries();
                            s.IssuedDate = _dateTime.Now;
                            s.HomeInternetBenefit = com1.HomeInternetBenefit;
                            s.InsuranceBenefit = com1.InsuranceBenefit;
                            s.AdditionalAgreedPart = com1.AdditionalAgreedPart;
                            s.BaseSalary = (prev_days * (com2.BaseSalary / month_days) + (cur_days * (com1.BaseSalary / month_days)));
                            s.CarBenefit = com1.CarBenefit;
                            s.EmissionBenefit = com1.EmissionBenefit;
                            s.PhoneBenefit = com1.PhoneBenefit;
                            s.TotalMonthlyPay = s.BaseSalary + s.AdditionalAgreedPart;
                            s.EmployeeIdentifier = com1.EmployeeIdentifier;
                            _context.CompensationSalaries.Add(s);
                        }
                        else
                        {
                            salary.IssuedDate = _dateTime.Now;
                            salary.HomeInternetBenefit = com1.HomeInternetBenefit;
                            salary.InsuranceBenefit = com1.InsuranceBenefit;
                            salary.AdditionalAgreedPart = com1.AdditionalAgreedPart;
                            salary.BaseSalary = (prev_days * (com2.BaseSalary / month_days) + (cur_days * (com1.BaseSalary / month_days)));
                            salary.CarBenefit = com1.CarBenefit;
                            salary.EmissionBenefit = com1.EmissionBenefit;
                            salary.PhoneBenefit = com1.PhoneBenefit;
                            salary.TotalMonthlyPay = com1.BaseSalary + com1.AdditionalAgreedPart;
                            _context.CompensationSalaries.Update(salary);
                        }
                    }
                    else
                    {
                        var salary = await _context.CompensationSalaries
                        .FirstOrDefaultAsync(c => c.EmployeeIdentifier == request.EmployeeIdentifier
                        && (c.IssuedDate.Year == _dateTime.Now.Year
                        && c.IssuedDate.Month == _dateTime.Now.Month));
                        if (salary == null)
                        {
                            CompensationSalaries s = new CompensationSalaries();
                            s.IssuedDate = _dateTime.Now;
                            s.HomeInternetBenefit = com1.HomeInternetBenefit;
                            s.InsuranceBenefit = com1.InsuranceBenefit;
                            s.AdditionalAgreedPart = com1.AdditionalAgreedPart;
                            s.BaseSalary = com1.BaseSalary;
                            s.CarBenefit = com1.CarBenefit;
                            s.EmissionBenefit = com1.EmissionBenefit;
                            s.PhoneBenefit = com1.PhoneBenefit;
                            s.TotalMonthlyPay = com1.TotalMonthlyPay;
                            s.EmployeeIdentifier = com1.EmployeeIdentifier;
                            _context.CompensationSalaries.Add(s);
                        }
                        else
                        {
                            salary.IssuedDate = _dateTime.Now;
                            salary.HomeInternetBenefit = com1.HomeInternetBenefit;
                            salary.InsuranceBenefit = com1.InsuranceBenefit;
                            salary.AdditionalAgreedPart = com1.AdditionalAgreedPart;
                            salary.BaseSalary = com1.BaseSalary;
                            salary.CarBenefit = com1.CarBenefit;
                            salary.EmissionBenefit = com1.EmissionBenefit;
                            salary.PhoneBenefit = com1.PhoneBenefit;
                            salary.TotalMonthlyPay = com1.TotalMonthlyPay;
                            _context.CompensationSalaries.Update(salary);
                        }
                    }
                }
                else if(c.Count > 0)
                {
                    var com = c.FirstOrDefault();
                    var salary = await _context.CompensationSalaries
                    .FirstOrDefaultAsync(c => c.EmployeeIdentifier == request.EmployeeIdentifier
                    && (c.IssuedDate.Year == _dateTime.Now.Year
                    && c.IssuedDate.Month == _dateTime.Now.Month));
                    if(salary == null)
                    {
                        CompensationSalaries s = new CompensationSalaries();
                        s.IssuedDate = _dateTime.Now;
                        s.HomeInternetBenefit = com.HomeInternetBenefit;
                        s.InsuranceBenefit = com.InsuranceBenefit;
                        s.AdditionalAgreedPart = com.AdditionalAgreedPart;
                        s.BaseSalary = com.BaseSalary;
                        s.CarBenefit = com.CarBenefit;
                        s.EmissionBenefit = com.EmissionBenefit;
                        s.PhoneBenefit = com.PhoneBenefit;
                        s.TotalMonthlyPay = com.TotalMonthlyPay;
                        s.EmployeeIdentifier = com.EmployeeIdentifier;
                        _context.CompensationSalaries.Add(s);
                    }
                    else
                    {
                        salary.IssuedDate = _dateTime.Now;
                        salary.HomeInternetBenefit = com.HomeInternetBenefit;
                        salary.InsuranceBenefit = com.InsuranceBenefit;
                        salary.AdditionalAgreedPart = com.AdditionalAgreedPart;
                        salary.BaseSalary = com.BaseSalary;
                        salary.CarBenefit = com.CarBenefit;
                        salary.EmissionBenefit = com.EmissionBenefit;
                        salary.PhoneBenefit = com.PhoneBenefit;
                        salary.TotalMonthlyPay = com.TotalMonthlyPay;
                        _context.CompensationSalaries.Update(salary);
                    }
                }
                else
                {
                    throw new Exception("No Record avialable for Employee Salary");
                }
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
        }
    }
}
