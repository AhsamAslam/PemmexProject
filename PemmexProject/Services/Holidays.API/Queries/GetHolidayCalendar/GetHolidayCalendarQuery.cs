using AutoMapper;
using Holidays.API.Common;
using Holidays.API.Database.Entities;
using Holidays.API.Dtos;
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
        private readonly ICommonHolidayDAL _context;
        private readonly IMapper _mapper;

        public GetHolidayCalendarQueryHandeler(ICommonHolidayDAL context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<HolidayCalendarDto>> Handle(GetHolidayCalendarQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var calendar = await _context.GetHolidayCalendar(request.countryName, DateTime.Now.Year);
                return _mapper.Map<List<HolidayCalendar>, List<HolidayCalendarDto>>(calendar);
            }
            catch(Exception)
            {
                throw;
            }
            
        }
    }
}
