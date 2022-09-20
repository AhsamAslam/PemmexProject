using System;
using AutoMapper;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using TaskManager.API.Commands.ApplyHoliday;
using TaskManager.API.Commands.SaveBonusSetting;
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
             .ForMember(d => d.BonusTask, opt => opt.MapFrom(s => s.ChangeBonus))
             .ForMember(d => d.BudgetPromotionTask, opt => opt.MapFrom(s => s.ChangeBudgetPromotion))
             .ForMember(d => d.EmployeeSoftTargetsDto, opt => opt.MapFrom(s => s.ChangeEmployeeSoftTargets))
             .ForMember(d => d.EmployeeHardTargetsDto, opt => opt.MapFrom(s => s.ChangeEmployeeHardTargets))

             //.ForMember(d => d.RequesterName, opt => opt.MapFrom(s => s.RequesterName))
             .ReverseMap();

            CreateMap<ApplyHolidayCommand, ChangeHoliday>()
                .ForMember(d => d.Description, opt => opt.MapFrom(s => s.taskDescription));


            CreateMap<CompensationTask,ChangeCompensation>().ReverseMap();
            CreateMap<GradeTask, ChangeGrade>().ReverseMap();
            CreateMap<HolidayTask, ChangeHoliday>().ReverseMap();
            CreateMap<ManagerTask, ChangeManager>().ReverseMap();
            CreateMap<TitleTask, ChangeTitle>().ReverseMap();
            CreateMap<BudgetPromotionTask, ChangeBudgetPromotion>()
                .ForMember(d => d.changeBudgetPromotionDetails, opt => opt.MapFrom(s => s.BudgetPromotionTaskDetail))
                .ReverseMap();
            CreateMap<ChangeBudgetPromotionDetail,BudgetPromotionTaskDetail>().ReverseMap();
            CreateMap<TeamTask, ChangeTeam>().ReverseMap();
            CreateMap<Dtos.BonusTask, Database.Entities.BonusTask>().ReverseMap();



            CreateMap<UpdateBudgetPromotionTask, ChangeBudgetPromotion>()
                .ForMember(d => d.changeBudgetPromotionDetails, opt => opt.MapFrom(s => s.BudgetPromotionTaskDetail))
                .ReverseMap();
            CreateMap<ChangeBudgetPromotionDetail, UpdateBudgetPromotionTaskDetail>().ReverseMap();
            



            CreateMap<ApprovalSettingDto, OrganizationApprovalSettings>()
                .ForMember(d => d.organizationApprovalSettingDetails , opt => opt.MapFrom(s => s.approvalSettingDetails))
                .ReverseMap();

            CreateMap<ApprovalSettingDetailsDto, OrganizationApprovalSettingDetail>().ReverseMap();

            CreateMap<BonusSettings, BonusSettingsDto>();
            CreateMap<SaveBonusSettingCommand, BonusSettings>();
            CreateMap<NotificationDto, Database.Entities.Notifications>().ReverseMap();

            CreateMap<EmployeeSoftTargetsDto, ChangeEmployeeSoftTargets>().ReverseMap();
            CreateMap<EmployeeHardTargetsDto, ChangeEmployeeHardTargets>().ReverseMap();
        }

    }
}
