using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Database.Context;
using Organization.API.Dtos;
using PemmexCommonLibs.Application.Extensions;
using PemmexCommonLibs.Application.Helpers;

namespace Organization.API.Queries.GetEmployee
{
    public class GetEmployeeQuery:IRequest<EmployeeResponse>
    {
        public string Id { get; set; }
    }

    public class GetEmployeeQueryHandeler : IRequestHandler<GetEmployeeQuery, EmployeeResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetEmployeeQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<EmployeeResponse> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                IEnumerable<EmployeeResponse> employees =
                        from e in _context.Employees.Where(e1 => e1.EmployeeIdentifier == request.Id && e1.IsActive == true)
                        join e1 in _context.Employees on e.ManagerIdentifier
                        equals e1.EmployeeIdentifier into ps
                        from p in ps.DefaultIfEmpty()
                        join b in _context.Businesses on e.BusinessId equals b.Id
                        join c in _context.CostCenters on e.CostCenterId equals c.CostCenterId

                        select new EmployeeResponse
                        {
                            EmployeeIdentifier = e.EmployeeIdentifier,
                            ManagerIdentifier = e.ManagerIdentifier,
                            BusinessIdentifier = e.OrganizationIdentifier,
                            OrganizationIdentifier = b.ParentBusinessId,
                            Country = e.country,
                            CountryCellNumber = e.CountryCellNumber,
                            Email = e.Email,
                            EmployeeDob = e.EmployeeDob.ToDateTime3(),
                            EmployeeId = e.EmployeeId,
                            Gender = e.Gender,
                            Grade = e.Grade,
                            Emp_Guid = e.Emp_Guid,
                            FirstName = e.FirstName,
                            LastName = e.LastName,
                            MiddleName = e.MiddleName,
                            JobFunction = Enum.GetName(e.JobFunction),
                            OrganizationCountry = b.OrganizationCountry,
                            Province = e.Province,
                            ManagerName = p == null ? "" : (p.FirstName + p.LastName),
                            Nationality = e.Nationality,
                            Title = e.Title,
                            CostCenterIdentifier = c.CostCenterIdentifier,
                            CostCenterName = c.CostCenterName,
                            FirstLanguage = e.FirstLanguage,
                            FirstLanguageSkills = e.FirstLanguageSkills,
                            SecondLanguage = e.SecondLanguage,
                            SecondLanguageSkills = e.SecondLanguageSkills,
                            Muncipality = e.Muncipality,
                            PhoneNumber = e.PhoneNumber,
                            HouseNumber = e.HouseNumber,
                            ThirdLanguage = e.ThirdLanguage,
                            ThirdLanguageSkills = e.ThirdLanguageSkills,
                            PostalCode = e.PostalCode,
                            Shift = e.Shift,
                            StreetAddress = e.StreetAddress,
                            Contacts = _mapper.Map<List<EmployeeContactResponse>>(e.employeeContacts)
                        };
                var q = employees.FirstOrDefault();
                return q;
                
            }
            catch(Exception)
            {
                throw;
            } 
        }
    }
}
