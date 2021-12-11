using Authentication.API.Database.Entities;
using Authentication.API.Dtos;
using AutoMapper;
using PemmexCommonLibs.Domain.Common.Dtos;
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
            CreateMap<RolesEntity, Roles>()
                .ForMember(d => d.Screens, opt => opt.MapFrom(s => s.screenEntities));

            CreateMap<User, UserDto>();
            CreateMap<User, TokenUser>().ReverseMap();

        }


    }
}
