using System;
using AutoMapper;
using PemmexAPIAggregator.Models;
using PemmexCommonLibs.Domain.Enums;

namespace PemmexAPIAggregator
{
    public class AutoMapperDto:Profile
    {
        public AutoMapperDto()
        {
            CreateMap<BusinessUnit,Employee>()
                   .ForMember(d => d.JobFunction,
                    opt => opt.MapFrom(s => Enum.GetName(typeof(JobFunction), s.JobFunction)));
          


        }

    }
}
