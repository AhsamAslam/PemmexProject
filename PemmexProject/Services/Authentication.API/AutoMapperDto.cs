using Authentication.API.Database.Entities;
using Authentication.API.Dtos;
using AutoMapper;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.API
{
    public class AutoMapperDto:Profile
    {
        public AutoMapperDto()
        {
            CreateMap<RolesEntity, Database.Entities.Roles>()
                .ForMember(d => d.Screens, opt => opt.MapFrom(s => s.screenEntities));

            CreateMap<User, UserDto>()
                .ForMember(d => d.JobFunction,
                    opt => opt.MapFrom(s => Enum.GetName(typeof(JobFunction), s.JobFunction)));
            CreateMap<User, TokenUser>()
                .ForMember(d => d.JobFunction,
                    opt => opt.MapFrom(s => Enum.GetName(typeof(JobFunction), s.JobFunction)));

        }
    }
}
