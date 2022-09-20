using System;
using AutoMapper;
using Organization.API.Database.Entities;
using Organization.API.Dtos;
using PemmexCommonLibs.Application.Extensions;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;

namespace Organization.API
{
    public class AutoMapperDto:Profile
    {
        public AutoMapperDto()
        {
            CreateMap<OrganizationRequestDto,Database.Entities.Organization>()
                .ForMember(d => d.OrganizationIdentifier, opt => opt.MapFrom(s => s.OrgNumber))
                .ForMember(d => d.OrganizationName, opt => opt.MapFrom(s => s.Name));

            CreateMap<Database.Entities.Organization, OrganizationVM>()
                .ForMember(d => d.OrganizationDetails, opt => opt.MapFrom(s => s.Businesses));

                        CreateMap<Business, BusinessVM>().ReverseMap();


            CreateMap<BusinessRequestDto, Business>()
                .ForMember(d => d.BusinessIdentifier, opt => opt.MapFrom(s => s.OrgNumber))
                .ForMember(d => d.BusinessName, opt => opt.MapFrom(s => s.Name))
                .ForMember(d => d.ParentBusinessId, opt => opt.MapFrom(s => s.ParentNumber));


            CreateMap<BusinessDetailRequest, Business>()
                .ForPath(d => d.Employees, opt => opt.MapFrom(s => s.Employees));


            CreateMap<EmployeeUploadRequest,UserEntity>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.Emp_Guid))
                .ForMember(d => d.JobFunction, opt => opt.MapFrom((s, d) => (s.JobFunction == null)
                ? JobFunction.General : s.JobFunction));



            CreateMap<HolidayUploadRequest, CompanyToEmployeeHolidayEntity>();

            CreateMap<EmployeeUploadRequest, Employee>()
                .ForMember(d => d.Emp_Guid, opt => opt.MapFrom(s => s.Emp_Guid))
                .ForMember(d => d.CostCenter, opt => opt.MapFrom(s => s.CostCenter))
                .ForMember(d => d.employeeContacts, opt => opt.MapFrom(s => s.employeeContacts))
                .ForMember(d => d.EmployeeDob, opt => opt.MapFrom(s => s.EmployeeDob.ToDateTime2()))
                .ForMember(d => d.FirstLanguageSkills,
                    opt => opt.MapFrom(s => Enum.GetName(typeof(LanguageSkills), s.FirstLanguageSkills)))
                .ForMember(d => d.SecondLanguageSkills,
                    opt => opt.MapFrom(s => Enum.GetName(typeof(LanguageSkills), s.SecondLanguageSkills)))
                .ForMember(d => d.JobFunction,
                    opt => opt.MapFrom(s => Enum.GetName(typeof(JobFunction), s.JobFunction)))
                .ForMember(d => d.ThirdLanguageSkills,
                    opt => opt.MapFrom(s => Enum.GetName(typeof(LanguageSkills), s.ThirdLanguageSkills)));
            
            CreateMap<EmployeeRequest, Employee>();
            CreateMap<Employee, EmployeeResponse>()
               .ForMember(d => d.Contacts, opt => opt.MapFrom(s => s.employeeContacts))
               .ForMember(d => d.JobFunction,
                    opt => opt.MapFrom(s => Enum.GetName(typeof(JobFunction), s.JobFunction)));
            CreateMap<CompensationUploadRequest, CompensationEntity>();
            CreateMap<CostCenter,CostCenterResponse>()
                .ForMember(d => d.CostCenterId, opt => opt.MapFrom(s => s.CostCenterId))
                .ForMember(d => d.CostCenterIdentifier, opt => opt.MapFrom(s => s.CostCenterIdentifier))
                .ForMember(d => d.CostCenterName, opt => opt.MapFrom(s => s.CostCenterName))
                .ForMember(d => d.ParentCostCenterIdentifier, opt => opt.MapFrom(s => s.ParentCostCenterIdentifier))
                .ForMember(d => d.businessIdentifier, opt => opt.MapFrom(s => s.businessIdentifier));
            CreateMap<CostCenter, CostCenterRequest>()
                .ForMember(d => d.CostCenterIdentifier, opt => opt.MapFrom(s => s.CostCenterIdentifier))
                .ForMember(d => d.CostCenterName, opt => opt.MapFrom(s => s.CostCenterName))
                .ForMember(d => d.ParentCostCenterIdentifier, opt => opt.MapFrom(s => s.ParentCostCenterIdentifier));
            CreateMap<CostCenterUploadRequest, CostCenter>();
 
            CreateMap<EmployeeContactUpload, EmployeeContacts>();
            CreateMap<EmployeeContactRequest, EmployeeContacts>();
            CreateMap<EmployeeContacts, EmployeeContactResponse>();
            
        }

    }
}
