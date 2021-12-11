using System;
using AutoMapper;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using TaskManager.API.Commands.ApplyHoliday;
using TaskManager.API.Database.Entities;
using TaskManager.API.Dtos;

namespace TaskManager.API
{
    public class AutoMapperDto:Profile
    {
        public AutoMapperDto()
        {
            CreateMap<BaseTask, TaskDto>()
             .ForMember(d => d.compensationTask, opt => opt.MapFrom(s => s.ChangeCompensation))
             .ForMember(d => d.GradeTask, opt => opt.MapFrom(s => s.ChangeGrade))
             .ForMember(d => d.holidayTask, opt => opt.MapFrom(s => s.ChangeHoliday))
             .ForMember(d => d.managerTask, opt => opt.MapFrom(s => s.ChangeManager))
             .ForMember(d => d.titleTask, opt => opt.MapFrom(s => s.ChangeTitle))
             .ForMember(d => d.TeamTask, opt => opt.MapFrom(s => s.ChangeTeam))
             .ReverseMap();

            CreateMap<ChangeHoliday, ApplyHolidayCommand>()
                .ReverseMap();


            CreateMap<CompensationTask,ChangeCompensation>().ReverseMap();
            CreateMap<GradeTask, ChangeGrade>().ReverseMap();
            CreateMap<HolidayTask, ChangeHoliday>().ReverseMap();
            CreateMap<ManagerTask, ChangeManager>().ReverseMap();
            CreateMap<TitleTask, ChangeTitle>().ReverseMap();
            CreateMap<TeamTask, ChangeTeam>().ReverseMap();

            CreateMap<ApprovalSettingDto, OrganizationApprovalSettings>()
                .ForMember(d => d.organizationApprovalSettingDetails , opt => opt.MapFrom(s => s.approvalSettingDetails))
                .ReverseMap();

            CreateMap<ApprovalSettingDetailsDto, OrganizationApprovalSettingDetail>().ReverseMap();
            CreateMap<NotificationDto, Notifications>().ReverseMap();
            

            //CreateMap<TaskDto,TaskEntity>()
            //    .ForMember(d => d.compensation, opt => opt.MapFrom(s => s.compensationTask))
            //    .ForMember(d => d.Grade, opt => opt.MapFrom(s => s.GradeTask))
            //    .ForMember(d => d.holiday, opt => opt.MapFrom(s => s.holidayTask))
            //    .ForMember(d => d.manager, opt => opt.MapFrom(s => s.managerTask))
            //    .ForMember(d => d.title, opt => opt.MapFrom(s => s.titleTask))
            //    .ReverseMap();

        }

    }
}
