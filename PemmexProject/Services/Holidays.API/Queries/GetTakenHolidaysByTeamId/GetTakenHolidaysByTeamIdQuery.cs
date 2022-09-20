using AutoMapper;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PemmexCommonLibs.Application.Extensions;
using PemmexCommonLibs.Application.Helpers;
using PemmexCommonLibs.Application.Interfaces;
using PemmexCommonLibs.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Holidays.API.Queries.GetTakenHolidaysByTeamId
{
    public class GetTakenHolidaysByTeamIdQuery : IRequest<List<EmployeeHolidayDto>>
    {
        public string[] employeeIdentifiers { get; set; }
        public string businessIdentifier { get; set; }
    }
    public class GetTakenHolidaysByEmployeeIdQueryHandeler : IRequestHandler<GetTakenHolidaysByTeamIdQuery, List<EmployeeHolidayDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDateTime _dateTime;
        private readonly IMapper _mapper;
        private readonly DbContextOptionsBuilder<HolidaysContext> optionsBuilder = null;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetTakenHolidaysByEmployeeIdQueryHandeler(IApplicationDbContext context, IDateTime dateTime,IMapper mapper,IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _dateTime = dateTime;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            this.optionsBuilder = new DbContextOptionsBuilder<HolidaysContext>()
            .UseSqlServer(configuration.GetConnectionString("HolidaysConnection"));
        }
        public async Task<List<EmployeeHolidayDto>> Handle(GetTakenHolidaysByTeamIdQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var setting = await _context.HolidaySettings
                    .Where(s => s.BusinessIdentifier == request.businessIdentifier)
                    .OrderByDescending(d => d.HolidayCalendarYear).FirstOrDefaultAsync();

                var holidays = await _context.CompanyToEmployeeHolidays
                    .Where(e => request.employeeIdentifiers.Contains(e.EmployeeIdentifier) && e.HolidaySettingsIdentitfier == setting.HolidaySettingsIdentitfier)
                    .ToListAsync(cancellationToken);


                if (holidays == null)
                    throw new Exception("No Holidays Found");


                List<EmployeeHolidayDto> takenHolidays = new List<EmployeeHolidayDto>();
                var tasks = holidays.Select(async e =>
                {
                    var h= await GetUsedHolidaysEmployee(e.EmployementStartDate.ToDateTime3(), setting, e.EmployeeIdentifier);
                    takenHolidays.AddRange(h);
                });
                await Task.WhenAll(tasks);
                return takenHolidays;

            }
            catch (Exception)
            {
                throw;
            }
        }
        private async Task<List<EmployeeHolidayDto>> GetUsedHolidaysEmployee(DateTime startedDate, HolidaySettings setting, string EmployeeId)
        {
            try
            {
                using (var db = new HolidaysContext(this.optionsBuilder.Options, _dateTime, _httpContextAccessor))
                {
                    DateTime start_calendar = startedDate > setting.HolidayCalendarYear ? startedDate : setting.HolidayCalendarYear;
                    DateTime end_calendar = setting.HolidayCalendarYear.AddYears(1);
                    var holidays = await db.EmployeeHolidays
                    .Where(h => h.HolidayStartDate >= start_calendar && h.HolidayStartDate <= end_calendar)
                    .Where(h => h.HolidayEndDate >= start_calendar && h.HolidayEndDate <= end_calendar)
                    .Where(h => (h.HolidayStatus == HolidayStatus.Planned 
                    || h.HolidayStatus == HolidayStatus.Approved 
                    || h.HolidayStatus == HolidayStatus.Availed))
                    .Where(h => h.EmployeeIdentifier == EmployeeId)
                    .ToListAsync();
                    var h = _mapper.Map<List<EmployeeHolidayDto>>(holidays);
                    h.ForEach(p =>
                    {
                        var start = (p.HolidayStartDate < start_calendar) ? start_calendar : p.HolidayStartDate;
                        var end = (p.HolidayEndDate > end_calendar ? end_calendar : p.HolidayEndDate);
                        int? days = (int?)((end - start)?.TotalDays == 0 ? 1 : (end - start)?.TotalDays);
                        p.TotalDays = days ?? 0;
                        
                    });

                    return h;
                }
                
            }
            catch (Exception)
            {
                throw;

            }
        }
    }
}
