using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Organization.API.Database.Context;
using Organization.API.Database.Entities;
using Organization.API.Dtos;
using Organization.API.Queries.GetOrganization;

namespace Organization.API.Queries.GetAllEmployeeTree
{
    public class GetAllEmployeeTree:IRequest<List<EmployeeResponse>>
    {
        public string Id { get; set; }
    }
    public class GetAllEmployeeTreeHandler : IRequestHandler<GetAllEmployeeTree, List<EmployeeResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllEmployeeTreeHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<EmployeeResponse>> Handle(GetAllEmployeeTree request, CancellationToken cancellationToken)
        {
            try
            {
                var org = await _context.Businesses
               .Include(e => e.Employees)
               .Where(e => e.IsActive == true && (e.ParentBusinessId == request.Id || e.BusinessIdentifier == request.Id))
               .ToListAsync(cancellationToken);
                List<EmployeeResponse> employeeResponses = new List<EmployeeResponse>();
                foreach (var business in org)
                {
                    var e = _mapper.Map<List<Employee>, List<EmployeeResponse>>(business.Employees.ToList());
                    employeeResponses.AddRange(e);
                }
                var recursiveData = FillRecursive(employeeResponses, "");
                return recursiveData;
            }
            catch(Exception)
            {
                throw;
            }
        }
        private static List<EmployeeResponse> FillRecursive(List<EmployeeResponse> employeeVms, string parentId)
        {
            try
            {
                return employeeVms.Where(x => x.ManagerIdentifier == parentId).Select(item => new EmployeeResponse()
                {
                    Country = item.Country,
                    CountryCellNumber = item.CountryCellNumber,
                    Email = item.Email,
                    EmployeeDob = item.EmployeeDob,
                    EmployeeIdentifier = item.EmployeeIdentifier,
                    FirstLanguage = item.FirstLanguage,
                    FirstLanguageSkills = item.FirstLanguageSkills,
                    FirstName = item.FirstName,
                    Gender = item.Gender,
                    Grade = item.Grade,
                    HouseNumber = item.HouseNumber,
                    EmployeeId = item.EmployeeId,
                    ManagerIdentifier = item.ManagerIdentifier,
                    SecondLanguageSkills = item.SecondLanguageSkills,
                    ThirdLanguage = item.ThirdLanguage,
                    ThirdLanguageSkills = item.ThirdLanguageSkills,
                    LastName = item.LastName,
                    Title = item.Title,
                    MiddleName = item.MiddleName,
                    Muncipality = item.Muncipality,
                    Nationality = item.Nationality,
                    PhoneNumber = item.PhoneNumber,
                    PostalCode = item.PostalCode,
                    Province = item.Province,
                    SecondLanguage = item.SecondLanguage,
                    StreetAddress = item.StreetAddress,
                    Emp_Guid = item.Emp_Guid,

                    children = FillRecursive(employeeVms, item.EmployeeIdentifier)

                }).ToList();
            }
            catch(Exception)
            {
                throw;
            }
        }

    }
}
  