using System;
using AutoMapper;
using Notifications.API.Commands.AnnounceNotification;
using Notifications.API.Commands.SaveNotification;
using Notifications.API.Dtos;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;

namespace Notifications.API
{
    public class AutoMapperDto:Profile
    {
        public AutoMapperDto()
        {
            CreateMap<NotificationDto, Database.Entities.Notifications>().ReverseMap();
            CreateMap<SaveNotificationCommand, Database.Entities.Notifications>()
                .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.userId));
            CreateMap<AnnounceNotificationCommand, AnnounceNotificationDto>()
                .ForMember(d => d.EmployeeId, opt => opt.MapFrom(s => s.userId));

        }

    }
}
