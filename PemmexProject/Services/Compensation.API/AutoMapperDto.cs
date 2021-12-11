using System;
using AutoMapper;
using Compensation.API.Database.Entities;
using Compensation.API.Dtos;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Domain.Common.Dtos;
using PemmexCommonLibs.Domain.Enums;

namespace Compensation.API
{
    public class AutoMapperDto:Profile
    {
        public AutoMapperDto()
        {
            CreateMap<JobCatalogue, JobCatalogueDto>().ReverseMap();
            CreateMap<Database.Entities.Compensation, CompensationDto>().ReverseMap();
            CreateMap<CompensationSalaries, CompensationSalariesDto>().ReverseMap();
            CreateMap<CompensationBonuses, CompensationBonusesDto>().ReverseMap();

        }

    }
}
