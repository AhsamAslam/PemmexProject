using AutoMapper;
using Holidays.API.Database.Entities;
using Holidays.API.Dtos;
using Holidays.API.Repositories.Interface;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Queries.GetHolidayCalendar
{
    public class GetHolidayCalendarQuery : IRequest<List<HolidayCalendarDto>>
    {
        public string countryName { get; set; }
    }
    public class GetHolidayCalendarQueryHandeler : IRequestHandler<GetHolidayCalendarQuery, List<HolidayCalendarDto>>
    {
        private readonly IHolidayCalendar _holidayCalendar;
        private readonly IMapper _mapper;

        public GetHolidayCalendarQueryHandeler(IHolidayCalendar holidayCalendar, IMapper mapper)
        {
            _holidayCalendar = holidayCalendar;
            _mapper = mapper;
        }
        public async Task<List<HolidayCalendarDto>> Handle(GetHolidayCalendarQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var calendar = await _holidayCalendar.GetHolidayCalendar(request.countryName, DateTime.Now.Year);
                return _mapper.Map<List<HolidayCalendar>, List<HolidayCalendarDto>>(calendar.ToList());
            }
            catch(Exception)
            {
                throw;
            }
            
        }
    }
}
