using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Commands.CreateEmployee;
using Organization.API.Dtos;
using Organization.API.Entities;
using Organization.API.Interfaces;
using Organization.API.Repositories.Interface;
using PemmexCommonLibs.Application.Exceptions;
using PemmexCommonLibs.Application.Helpers;

namespace Organization.API.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommand : IRequest<int>
    {
        public EmployeeRequest employee { get; set; }
        public string Id { get; set; }
    }

    public class UpdateEmployeeCommandHandeler : IRequestHandler<UpdateEmployeeCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IEmployee _employee;
        private readonly IMapper _mapper;
        public UpdateEmployeeCommandHandeler(IApplicationDbContext context, IEmployee employee, IMapper mapper)
        {
            _context = context;
            _employee = employee;
            _mapper = mapper;
        }
        public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var guid = Guid.Parse(request.Id);
                //var e = await _context.Employees.Where(i => i.Emp_Guid == guid)
                //    .FirstOrDefaultAsync(cancellationToken);
                var e = await _employee.GetEmployeeByGuidId(guid); 
                if (e == null)
                {
                    throw new NotFoundException(nameof(Employee), request.Id);
                }

                e.ManagerIdentifier = request.employee.ManagerIdentifier;
                e.country = request.employee.Country;
                e.CountryCellNumber = request.employee.CountryCellNumber;
                e.Email = request.employee.Email;
                e.EmployeeDob = request.employee.EmployeeDob;
                e.EmployeeIdentifier = request.employee.EmployeeIdentifier;
                e.FirstLanguage = request.employee.FirstLanguage;
                e.FirstLanguageSkills = nameof(request.employee.FirstLanguageSkills);
                e.FirstName = request.employee.FirstName;
                e.Gender = request.employee.Gender;
                e.Grade = request.employee.Grade;
                e.HouseNumber = request.employee.HouseNumber;
                e.EmployeeId = request.employee.Id;
                e.ManagerIdentifier = request.employee.ManagerIdentifier;
                e.SecondLanguageSkills = nameof(request.employee.SecondLanguageSkills);
                e.ThirdLanguage = request.employee.ThirdLanguage;
                e.ThirdLanguageSkills = nameof(request.employee.ThirdLanguageSkills);
                e.LastName = request.employee.LastName;
                e.Title = request.employee.Title;
                e.MiddleName = request.employee.MiddleName;
                e.Muncipality = request.employee.Muncipality;
                e.Nationality = request.employee.Nationality;
                e.PhoneNumber = request.employee.PhoneNumber;
                e.PostalCode = request.employee.PostalCode;
                e.Province = request.employee.Province;
                e.SecondLanguage = request.employee.SecondLanguage;
                e.StreetAddress = request.employee.StreetAddress;
                e.IsActive = true;
                //if (request.employee.Compensation != null)
                //{

                //    Compensation compensation = new Compensation
                //    {
                //        EmployeeId = e.EmployeeId,
                //        AdditionalAgreedPart = request.employee.Compensation.AdditionalAgreedPart,
                //        BaseSalary = request.employee.Compensation.BaseSalary,
                //        CarBenefit = request.employee.Compensation.CarBenefit,
                //        EmissionBenefit = request.employee.Compensation.EmissionBenefit,
                //        HomeInternetBenefit = request.employee.Compensation.HomeInternetBenefit,
                //        InsuranceBenefit = request.employee.Compensation.InsuranceBenefit,
                //        PhoneBenefit = request.employee.Compensation.PhoneBenefit,
                //        EffectiveDate = request.employee.Compensation.EffectiveDate
                //    };
                //    e.Compensation.Add(compensation);
                //}
                //if (request.employee.CostCenter != null)
                //{
                //    e.CostCenter.ParentCostCenterIdentifier = request.employee.CostCenter.ParentCostCenterIdentifier;
                //    e.CostCenter.CostCenterName = request.employee.CostCenter.CostCenterName;

                //}
                //await _context.SaveChangesAsync(cancellationToken);
                var employee = await _employee.UpdateEmployee(e);
                return e.EmployeeId;
            }
            catch(Exception)
            {
                throw;
            }
        }
    }
}
