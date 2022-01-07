using AutoMapper;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Dtos;
using Holidays.API.Repositories.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Queries.GetHolidaySettings
{
    public class GetHolidaySettingsQuery : IRequest<List<HolidaySettingsDto>>
    {
        public string Id { get; set; }
    }
    public class GetHolidaySettingsQueryHandeler : IRequestHandler<GetHolidaySettingsQuery, List<HolidaySettingsDto>>
    {
        private readonly IHolidaySettings _holidaySetting;
        private readonly IMapper _mapper;

        public GetHolidaySettingsQueryHandeler(IHolidaySettings holidaySetting, IMapper mapper)
        {
            _holidaySetting = holidaySetting;
            _mapper = mapper;
        }
        public async Task<List<HolidaySettingsDto>> Handle(GetHolidaySettingsQuery request, CancellationToken cancellationToken)
        {
            var holidaySettings = await _holidaySetting.GetHolidaySettingsById(request.Id);

            return _mapper.Map<List<HolidaySettings>, List<HolidaySettingsDto>>(holidaySettings.ToList());
        }
    }
}
