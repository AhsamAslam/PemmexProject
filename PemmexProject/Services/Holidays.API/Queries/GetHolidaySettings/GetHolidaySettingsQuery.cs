using AutoMapper;
using Holidays.API.Database.context;
using Holidays.API.Database.Entities;
using Holidays.API.Dtos;
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
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetHolidaySettingsQueryHandeler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<HolidaySettingsDto>> Handle(GetHolidaySettingsQuery request, CancellationToken cancellationToken)
        {
            var holidaySettings = await _context.HolidaySettings
                .Where(e => e.OrganizationIdentifier == request.Id)
                .ToListAsync();

            return _mapper.Map<List<HolidaySettings>, List<HolidaySettingsDto>>(holidaySettings);
        }
    }
}
