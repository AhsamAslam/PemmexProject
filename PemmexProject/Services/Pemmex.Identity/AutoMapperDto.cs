using AutoMapper;
using Pemmex.Identity.Commands.SaveUser;
using Pemmex.Identity.Dtos;
using Pemmex.Identity.Models;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pemmex.Identity
{
    public class AutoMapperDto:Profile
    {
        public AutoMapperDto()
        {
            CreateMap<SaveUserCommand,ApplicationUser>();
            CreateMap<ApplicationUser,UserDto>();
        }
    }
}
