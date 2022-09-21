using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Nager.Date;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Commands.SaveCalendar
{
    public class CreateCalendarCommand : IRequest
    {
        public string countrycode { get; set; }
    }

    public class CreateCalendarCommandHandeler : IRequestHandler<CreateCalendarCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateCalendarCommandHandeler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Unit> Handle(CreateCalendarCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var year = DateTime.Now.Year;
                var holidays = await _context.HolidayCalendars.Where(h => h.Date.Year == year && h.CountryCode == request.countrycode).ToListAsync();
                if (holidays.Count != 0)
                {
                    _context.HolidayCalendars.RemoveRange(holidays);
                    await _context.SaveChangesAsync(cancellationToken);
                }
                var publicHolidays = DateSystem.GetPublicHolidays(year, request.countrycode);
                List<HolidayCalendar> calendars = new List<HolidayCalendar>();
                foreach (var publicHoliday in publicHolidays)
                {
                    HolidayCalendar calendar = new HolidayCalendar
                    {
                        Date = publicHoliday.Date,
                        Fixed = publicHoliday.Fixed,
                        Global = publicHoliday.Global,
                        LocalName = publicHoliday.LocalName,
                        CountryCode = publicHoliday.CountryCode.ToString(),
                        Name = publicHoliday.Name,
                        Type = publicHoliday.Type.ToString()
                    };
                    calendars.Add(calendar);
                }


                await _context.HolidayCalendars.AddRangeAsync(calendars);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
