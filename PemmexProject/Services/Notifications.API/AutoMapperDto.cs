using System;
using AutoMapper;
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

        }

    }
}
